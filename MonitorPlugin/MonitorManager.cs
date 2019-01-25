using Inventor;
using Monitor_Plugin.Inventor_API;
using Monitor_Plugin.Parameters;
using System;

namespace Monitor_Plugin
{
    /// <summary>
    /// Monitor making class
    /// </summary>
    public class MonitorManager
    {
        #region Public Methods

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="api">Wrapper API Inventor</param>
        /// <param name="modelParameters">Monitor parameters</param>
        public MonitorManager(InventorAPI api, MonitorParameters modelParameters)
        {
            _api = api;
            _modelParameters = modelParameters;
            _objCollection = _api.InventorApplication.TransientObjects.
                CreateObjectCollection();
        }

        /// <summary>
        /// Making an entire monitor model
        /// </summary>
        /// <param name="createBackFlag">Flag for back building</param>
        public void CreateMonitor(bool createBackFlag)
        {
            if (_modelParameters != null)
            {
                CreateStand();
                CreateLeg();
                CreateScreen(createBackFlag);
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Monitor parameters
        /// </summary>
        private MonitorParameters _modelParameters;

        /// <summary>
        /// Wrapper API Inventor
        /// </summary>
        private InventorAPI _api;

        /// <summary>
        /// Collection of sketches for creating strikers
        /// </summary>
        private ObjectCollection _objCollection;

        #endregion

        #region Private Methods

        /// <summary>
        /// Making a monitor stand in Inventor 2018
        /// </summary>
        private void CreateStand()
        {
            _api.MakeNewSketch(2, 0);
            _api.DrawCircle(0, 0, _modelParameters.StandParam.Diameter);
            _api.Extrude(_modelParameters.StandParam.Height);
        }

        /// <summary>
        /// Making a monitor leg in Inventor 2018
        /// </summary>
        private void CreateLeg()
        {
            _api.MakeNewSketch(2, _modelParameters.StandParam.Height);

            _api.DrawRectangle(-(_modelParameters.LegParam.Width / 2),
	            _modelParameters.LegParam.Thikness / 2,
	            _modelParameters.LegParam.Width / 2,
	            -(_modelParameters.LegParam.Thikness / 2));

            _api.Extrude(_modelParameters.LegParam.Height);
        }

        /// <summary>
        /// Making a monitor screen in Inventor 2018
        /// </summary>
        /// <param name="createBackFlag">Flag for back building.
        /// If true - create back of monitor with perforation</param>
        private void CreateScreen(bool createBackFlag)
        {
            // Create screen
            _api.MakeNewSketch(2, _modelParameters.StandParam.Height +
                                  _modelParameters.LegParam.Height);

            _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2),
	            _modelParameters.ScreenParam.Thikness / 2,
	            _modelParameters.ScreenParam.Width / 2,
	            -(_modelParameters.ScreenParam.Thikness / 2));

            _api.Extrude(_modelParameters.ScreenParam.Height);

            // Matrix cutting
            _api.MakeNewSketch(3, _modelParameters.ScreenParam.Thikness / 2);

	        double frameThikness = 10;

            _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2) + frameThikness,
	            _modelParameters.StandParam.Height + _modelParameters.LegParam.Height +
	            _modelParameters.ScreenParam.Height - frameThikness,
                (_modelParameters.ScreenParam.Width / 2) - frameThikness,
	            _modelParameters.StandParam.Height + _modelParameters.LegParam.Height +
	            frameThikness);

            _api.CutOut(5, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

	        //Сreate some buttons
	        _api.MakeNewSketch(3, _modelParameters.ScreenParam.Thikness / 2);

	        for (int i = 0; i <= 2; i++)
	        {
				// Variables for buttons create
		        double distanceFromBorderSide = 15;
		        double distanceFromDownSide = 5;
		        double distanceBetweenButtons = 5;
		        double buttonsDiameter = 3;

		        _api.DrawCircle(_modelParameters.ScreenParam.Width / 2 -
		                        (distanceFromBorderSide + distanceBetweenButtons * i),
			        _modelParameters.StandParam.Height + _modelParameters.LegParam.Height +
			        distanceFromDownSide, buttonsDiameter);
	        }

	        double buttonsThikness = 1;

			_api.Extrude(buttonsThikness);

			// Create back
			if (createBackFlag)
            {
                _api.MakeNewSketch(3, -(_modelParameters.ScreenParam.Thikness / 2));

                _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2),
	                _modelParameters.StandParam.Height + _modelParameters.LegParam.Height +
	                _modelParameters.ScreenParam.Height, _modelParameters.ScreenParam.Width / 2,
	                _modelParameters.StandParam.Height + _modelParameters.LegParam.Height);

                _objCollection.Add(_api.CurrentSketch.Profiles.AddForSolid());

	            double backWidthAccess = 20;

                _api.MakeNewSketch(3, -(_modelParameters.ScreenParam.Thikness / 2) - backWidthAccess);

                _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2) + frameThikness,
	                _modelParameters.StandParam.Height + _modelParameters.LegParam.Height +
	                _modelParameters.ScreenParam.Height - frameThikness,
	                _modelParameters.ScreenParam.Width / 2 - frameThikness,
	                _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + frameThikness);

                _objCollection.Add(_api.CurrentSketch.Profiles.AddForSolid());
                _api.Loft(_objCollection);
                _objCollection.Clear();

				// Perforation cells width
				double cellsWidth = 2;

				// Distance between perforation cells
				double cellDistance = 2;

				// Number of perforation cells of perforation
				double backPerforationCount = 
					Math.Floor((_modelParameters.ScreenParam.Width - backWidthAccess) / (cellsWidth + cellDistance));

                //Create perforation cells
                _api.MakeNewSketch(3, -(_modelParameters.ScreenParam.Thikness / 2) - frameThikness * 2);

                for (int i = 0; i < backPerforationCount; i++)
                {
                    _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2) + frameThikness + 2 + i * 4,
	                    _modelParameters.StandParam.Height + _modelParameters.LegParam.Height +
	                    _modelParameters.ScreenParam.Height,
	                    -(_modelParameters.ScreenParam.Width / 2) + frameThikness + 4 + i * 4,
	                    _modelParameters.StandParam.Height + _modelParameters.LegParam.Height +
	                    _modelParameters.ScreenParam.Height - (0.3 * _modelParameters.ScreenParam.Height));
                }

                _api.CutOut(9);
            }
        }

        #endregion
    }
}