namespace Monitor_Plugin.Parameters
{
    public class ScreenParameters
    {
        #region Public Methods

        public ScreenParameters(double height, double width, double thikness)
        {
            _height = height;
            _width = width;
            _thikness = thikness;
        }

        #endregion

        #region Properties

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