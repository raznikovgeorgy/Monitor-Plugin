namespace Monitor_Plugin.Parameters
{
    /// <summary>
    /// Parameters of monitor screen
    /// </summary>
    public class ScreenParameters
    {
        #region Public Methods

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="height">Monitor screen height</param>
        /// <param name="width">Monitor screen width</param>
        /// <param name="thikness">Monitor screen thikness</param>
        public ScreenParameters(double height, double width, double thikness)
        {
            _height = height;
            _width = width;
            _thikness = thikness;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Monitor screen height
        /// </summary>
        public double Height { get => _height; }

        /// <summary>
        /// Monitor screen width
        /// </summary>
        public double Width { get => _width; }

        /// <summary>
        /// Monitor screen thikness
        /// </summary>
        public double Thikness { get => _thikness; }

        #endregion

        #region Fields

        /// <summary>
        /// Monitor screen height
        /// </summary>
        private double _height;

        /// <summary>
        /// Monitor screen width
        /// </summary>
        private double _width;

        /// <summary>
        /// Monitor screen thikness
        /// </summary>
        private double _thikness;

        #endregion
    }
}