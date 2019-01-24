using Inventor;
using Monitor_Plugin.Inventor_API;
using Monitor_Plugin.Parameters;

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
            _api.DrawRectangle(-(_modelParameters.LegParam.Width / 2), _modelParameters.LegParam.Thikness / 2, _modelParameters.LegParam.Width / 2, -(_modelParameters.LegParam.Thikness / 2));
            _api.Extrude(_modelParameters.LegParam.Height);
        }

        /// <summary>
        /// Making a monitor screen in Inventor 2018
        /// </summary>
        /// <param name="createBackFlag">Flag for back building. If true - create back of monitor with perforation</param>
        private void CreateScreen(bool createBackFlag)
        {
            // Create screen
            _api.MakeNewSketch(2, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height);
            _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2), _modelParameters.ScreenParam.Thikness / 2, _modelParameters.ScreenParam.Width / 2, -(_modelParameters.ScreenParam.Thikness / 2));
            _api.Extrude(_modelParameters.ScreenParam.Height);

            // Matrix cutting
            _api.MakeNewSketch(3, _modelParameters.ScreenParam.Thikness / 2);
            _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2) + 10, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + _modelParameters.ScreenParam.Height - 10,
                (_modelParameters.ScreenParam.Width / 2) - 10, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + 10);
            _api.CutOut(5, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

            // Create back
            if (createBackFlag)
            {
                _api.MakeNewSketch(3, -(_modelParameters.ScreenParam.Thikness / 2));
                _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2) + 10, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + _modelParameters.ScreenParam.Height - 10,
                    (_modelParameters.ScreenParam.Width / 2) - 10, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + 10);
                _api.Extrude(15, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

                // Number of perforation cells
                double backPerforationCount = (_modelParameters.ScreenParam.Width - 20) / 4;

                //Create perforation cells
                _api.MakeNewSketch(3, -(_modelParameters.ScreenParam.Thikness / 2) - 15);
                for (int i = 0; i <= backPerforationCount; i++)
                {
                    _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2) + 12 + i * 4, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + _modelParameters.ScreenParam.Height - 10,
                        -(_modelParameters.ScreenParam.Width / 2) + 14 + i * 4, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + _modelParameters.ScreenParam.Height - 80);
                }
                _api.CutOut(9);
            }

            //Сreate some buttons
            _api.MakeNewSketch(3, _modelParameters.ScreenParam.Thikness / 2);
            for (int i = 0; i <= 2; i++)
            {
                _api.DrawCircle(_modelParameters.ScreenParam.Width / 2 - (15 + i * 5), _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + 5, 3);
            }
            _api.Extrude(1);
        }

        #endregion
    }
}