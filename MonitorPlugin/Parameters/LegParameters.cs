namespace Monitor_Plugin.Parameters
{
    /// <summary>
    /// Parameters of monitor leg
    /// </summary>
    public class LegParameters
    {
        #region Public Methods

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="height">Monitor leg height</param>
        /// <param name="width">Monitor leg width</param>
        /// <param name="thikness">Monitor leg thikness</param>
        public LegParameters(double height, double width, double thikness)
        {
            _height = height;
            _width = width;
            _thikness = thikness;
        }

        #endregion

        #region Propetries

        /// <summary>
        /// Monitor leg height
        /// </summary>
        public double Height { get => _height; }

        /// <summary>
        /// Monitor leg width
        /// </summary>
        public double Width { get => _width; }

        /// <summary>
        /// Monitor leg thikness
        /// </summary>
        public double Thikness { get => _thikness; }

        #endregion

        #region Fields

        /// <summary>
        /// Monitor leg height
        /// </summary>
        private double _height;

        /// <summary>
        /// Monitor leg width
        /// </summary>
        private double _width;

        /// <summary>
        /// Monitor leg thikness
        /// </summary>
        private double _thikness;

        #endregion
    }
}