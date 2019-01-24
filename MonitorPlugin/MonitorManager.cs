using Inventor;
using Monitor_Plugin.Inventor_API;
using Monitor_Plugin.Parameters;

namespace Monitor_Plugin
{
    public class MonitorManager
    {
        #region Public Methods

        public MonitorManager(InventorAPI api, MonitorParameters modelParameters)
        {
            _api = api;
            _modelParameters = modelParameters;
        }

        public void CreateMonitor()
        {
            if (_modelParameters != null)
            {
                CreateStand();
                CreateLeg();
                CreateScreen();
            }
        }

        #endregion

        #region Fields

        private MonitorParameters _modelParameters;
        private InventorAPI _api;

        #endregion

        #region Private Methods

        private void CreateStand()
        {
            _api.MakeNewSketch(2, 0);
            _api.DrawCircle(0, 0, _modelParameters.StandParam.Diameter);
            _api.Extrude(_modelParameters.StandParam.Height);
        }

        private void CreateLeg()
        {
            _api.MakeNewSketch(2, _modelParameters.StandParam.Height);
            _api.DrawRectangle(-(_modelParameters.LegParam.Width / 2), _modelParameters.LegParam.Thikness / 2, _modelParameters.LegParam.Width / 2, -(_modelParameters.LegParam.Thikness / 2));
            _api.Extrude(_modelParameters.LegParam.Height);
        }

        private void CreateScreen()
        {
            _api.MakeNewSketch(2, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height);
            _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2), _modelParameters.ScreenParam.Thikness / 2, _modelParameters.ScreenParam.Width / 2, -(_modelParameters.ScreenParam.Thikness / 2));
            _api.Extrude(_modelParameters.ScreenParam.Height);

            //MatrixCutting
            _api.MakeNewSketch(3, _modelParameters.ScreenParam.Thikness / 2);
            _api.DrawRectangle(-(_modelParameters.ScreenParam.Width / 2) + 10, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + _modelParameters.ScreenParam.Height - 10,
                (_modelParameters.ScreenParam.Width / 2) - 10, _modelParameters.StandParam.Height + _modelParameters.LegParam.Height + 10);
            _api.CutOut(5, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

        }

        #endregion
    }
}