namespace Monitor_Plugin.Parameters
{
    /// <summary>
    /// Parameters of monitor stand
    /// </summary>
    public class StandParameters
    {
        #region Public Methods

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="height">Monitor stand height</param>
        /// <param name="diameter">Monitor stand diameter</param>
        public StandParameters(double height, double diameter)
        {
            _height = height;
            _diameter = diameter;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Monitor stand height
        /// </summary>
        public double Height { get => _height; }

        /// <summary>
        /// Monitor stand diameter
        /// </summary>
        public double Diameter { get => _diameter; }

        #endregion

        #region Fields

        /// <summary>
        /// Monitor stand diameter
        /// </summary>
        private double _height;

        /// <summary>
        /// Monitor stand diameter
        /// </summary>
        private double _diameter;

        #endregion
    }
}