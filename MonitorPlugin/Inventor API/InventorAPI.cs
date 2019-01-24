using System;
using Inventor;
using Application = Inventor.Application;
using System.Runtime.InteropServices;

namespace Monitor_Plugin.Inventor_API
{
    public class InventorAPI
    {
        // ****************
        // Public
        #region Public Methods

        /// <summary>
        /// Initialize an Inventor and prepare variables
        /// </summary>
        public InventorAPI()
        {
            try
            {
                // Trying to seize control of the inventory application
                InventorApplication = (Application)Marshal.
                    GetActiveObject("Inventor.Application");
            }
            catch (Exception)
            {
                try
                {
                    // If the application could not be intercepted, it will 
                    // be thrown out that there is no such application. Let's
                    // try to create an application manually
                    Type _inventorApplicationType = Type.
                        GetTypeFromProgID("Inventor.Application");

                    InventorApplication = (Application)Activator.
                        CreateInstance(_inventorApplicationType);
                    InventorApplication.Visible = true;
                }
                catch (Exception)
                {
                    // If nothing happened, we’ll discard the message that 
                    // the inventory is not installed, or for some reason 
                    // did not manage to reach it.
                    PluginReporter.Instance().Add(
                        PluginReporter.TypeError.ErrorAPI,
                        "Failed to start Inventor.");
                }
            }

            Initialization(InventorApplication);
        }


        /// <summary>
        /// The method of initializing the assembly document
        /// </summary>
        /// <param name="InventorApplication"> Link to the application </param>
        public void Initialization(Application InventorApplication)
        {
            if (InventorApplication == null)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorAPI,
                    "Failed to start Inventor");
                return;
            }

            // In the open application, create a metric assembly
            _partDoc = (PartDocument)InventorApplication.Documents.Add
                (DocumentTypeEnum.kPartDocumentObject,
                InventorApplication.FileManager.
                GetTemplateFile(DocumentTypeEnum.kPartDocumentObject,
                    SystemOfMeasureEnum.kMetricSystemOfMeasure));

            // Document description
            _partDef = _partDoc.ComponentDefinition;

            // Initialize the geometry method
            _transGeometry = InventorApplication.TransientGeometry;
        }


        /// <summary>
        /// Creating a plane by transferring the ZY, ZX, XY planes
        /// </summary>
        /// <param name="n"> Plane number: 1 - ZY, 2 - ZX, 3 - XY </param>
        /// <param name="offset"> Relative plane offset </param>
        public PlanarSketch MakeNewSketch(int n, double offset)
        {
            // Get the reference to the work plane
            var mainPlane = _partDef.WorkPlanes[n];

            // Make the shifted plane.
            var offsetPlane = _partDef.WorkPlanes.
                AddByPlaneAndOffset(mainPlane, offset / _toMillimiters);

            // Create a sketch on the plane.
            _currentSketch = _partDef.Sketches.Add(offsetPlane);

            offsetPlane.Visible = false;

            return _currentSketch;
        }


        /// <summary>
        /// Draws a two-point rectangle
        /// </summary>
        /// <param name="pointOneX"> Left upper X coordinate </param>
        /// <param name="pointOneY"> Left upper Y coordinate </param>
        /// <param name="pointTwoX"> Lower right X coordinate </param>
        /// <param name="pointTwoY"> Lower right Y coordinate </param>
        public void DrawRectangle(double pointOneX, double pointOneY,
            double pointTwoX, double pointTwoY)
        {
            pointOneX /= _toMillimiters;
            pointOneY /= _toMillimiters;
            pointTwoX /= _toMillimiters;
            pointTwoY /= _toMillimiters;

            var cornerPointOne = _transGeometry.
                CreatePoint2d(pointOneX, pointOneY);
            var cornerPointTwo = _transGeometry.
                CreatePoint2d(pointTwoX, pointTwoY);

            _currentSketch.SketchLines.
                AddAsTwoPointRectangle(cornerPointOne, cornerPointTwo);
        }


        /// <summary>
        /// Draws a circle
        /// </summary>
        /// <param name="centerPointX"> X-center of the circle center </param>
        /// <param name="centerPointY"> Y-center of the circle </param>
        /// <param name="diameter"> Circle diameter </param>
        public void DrawCircle(double centerPointX, double centerPointY,
            double diameter)
        {
            centerPointX /= _toMillimiters;
            centerPointY /= _toMillimiters;
            diameter /= _toMillimiters;

            _currentSketch.SketchCircles.
                AddByCenterRadius(_transGeometry.
                CreatePoint2d(centerPointX, centerPointY),
                0.5 * diameter);
        }


        /// <summary>
        /// Extrusion
        /// </summary>
        /// <param name="distance"> Extrusion length </param>
        /// <param name="extrudeDirection"> Extrusion direction </param>
        public void Extrude(double distance,
            PartFeatureExtentDirectionEnum extrudeDirection =
            PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        {
            var extrudeDef = _partDef.Features.ExtrudeFeatures.
                CreateExtrudeDefinition(_currentSketch.Profiles.AddForSolid(),
                PartFeatureOperationEnum.kJoinOperation);
            extrudeDef.SetDistanceExtent(distance / _toMillimiters, extrudeDirection);
            _partDef.Features.ExtrudeFeatures.Add(extrudeDef);
        }


        /// <summary>
        /// Cutting
        /// </summary>
        /// <param name="distance"> Cut length </param>
        /// <param name="extrudeDirection"> Cutting direction</param>
        public void CutOut(double distance,
            PartFeatureExtentDirectionEnum extrudeDirection =
            PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        {
            var extrudeDef = _partDef.Features.ExtrudeFeatures.
                CreateExtrudeDefinition(_currentSketch.Profiles.AddForSolid(),
                PartFeatureOperationEnum.kCutOperation);
            extrudeDef.SetDistanceExtent(distance / _toMillimiters, extrudeDirection);
            _partDef.Features.ExtrudeFeatures.Add(extrudeDef);
        }


        /// <summary>
        /// Loft
        /// </summary>
        /// <param name="objCollection"> Loft planes </param>
        /// <param name="loftOperation"> Loft operation </param>
        public void Loft(ObjectCollection objCollection,
            PartFeatureOperationEnum loftOperation =
            PartFeatureOperationEnum.kJoinOperation)
        {
            var loftDef = _partDef.Features.LoftFeatures.
                CreateLoftDefinition(objCollection, loftOperation);

            var oLoft = _partDef.Features.LoftFeatures.Add(loftDef);
        }

        #endregion


        // ****************
        // Properties
        #region Properties

        /// <summary>
        /// Current sketch
        /// </summary>
        public PlanarSketch CurrentSketch
        {
            get
            {
                return _currentSketch;
            }
        }


        /// <summary>
        /// Document description
        /// </summary>
        public PartComponentDefinition PartDef { get; private set; }


        /// <summary>
        /// Link to the Inventor app
        /// </summary>
        public Application InventorApplication { get; set; }

        #endregion


        // ****************
        // Private
        #region Private Field

        /// <summary>
        /// Document in the app
        /// </summary>
        private PartDocument _partDoc;

        /// <summary>
        /// Document description
        /// </summary>
        private PartComponentDefinition _partDef;

        /// <summary>
        /// Application geometry
        /// </summary>
        private TransientGeometry _transGeometry;

        /// <summary>
        /// Current sketch
        /// </summary>
        private PlanarSketch _currentSketch;


        /// <summary>
        /// Сonstant to convert to millimeters
        /// </summary>
        private const double _toMillimiters = 10.0;

        #endregion
    }
}
