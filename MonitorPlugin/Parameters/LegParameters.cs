namespace Monitor_Plugin.Parameters
{
    public class LegParameters
    {
        #region Public Methods

        public LegParameters(double height, double width, double thikness)
        {
            _height = height;
            _width = width;
            _thikness = thikness;
        }

        #endregion

        #region Propetries

        public double Height { get => _height; }

        public double Width { get => _width; }

        public double Thikness { get => _thikness; }

        #endregion

        #region Fields

        private double _height;
        private double _width;
        private double _thikness;

        #endregion
    }
}