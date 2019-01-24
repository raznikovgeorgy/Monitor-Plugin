namespace Monitor_Plugin.Parameters
{
    public class StandParameters
    {
        #region Public Methods

        public StandParameters(double height, double diameter)
        {
            _height = height;
            _diameter = diameter;
        }

        #endregion

        #region Properties

        public double Height { get => _height; }

        public double Diameter { get => _diameter; }

        #endregion

        #region Fields

        private double _height;
        private double _diameter;

        #endregion
    }
}