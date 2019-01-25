using System.Collections.Generic;

namespace Monitor_Plugin.Parameters
{
    /// <summary>
    /// Monitor parameters storage class
    /// </summary>
    public class MonitorParameters
    {
        #region Public Methods

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="monitorParameters">List of all monitor parameters</param>
        public MonitorParameters(List<double> monitorParameters)
        {
            StandParameters standParameters = new StandParameters(
                monitorParameters[(int)ParametersType.StandHeight],
                monitorParameters[(int)ParametersType.StandDiameter]);

            LegParameters legParameters = new LegParameters(
                monitorParameters[(int)ParametersType.LegHeight],
                monitorParameters[(int)ParametersType.LegWidth],
                monitorParameters[(int)ParametersType.LegThikness]);

            ScreenParameters screenParameters = new ScreenParameters(
               monitorParameters[(int)ParametersType.ScreenHeight],
               monitorParameters[(int)ParametersType.ScreenWidth],
               monitorParameters[(int)ParametersType.ScreenThikness]);

            if (Validate(standParameters, legParameters, screenParameters))
            {
                _standParameters = standParameters;
                _legParameter = legParameters;
                _screenParameters = screenParameters;
            }
        }

        #endregion

        #region Propetries

        /// <summary>
        /// Monitor stand parameters
        /// </summary>
        public StandParameters StandParam { get => _standParameters; }

        /// <summary>
        /// Monitor leg parameters
        /// </summary>
        public LegParameters LegParam { get => _legParameter; }

        /// <summary>
        /// Monitor screen parameters
        /// </summary>
        public ScreenParameters ScreenParam { get => _screenParameters; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checking model parameters
        /// </summary>
        /// <param name="standParameters">Monitor stand parameters</param>
        /// <param name="legParameters">Monitor leg parameters</param>
        /// <param name="screenParameters">Monitor screen parameters</param>
        /// <returns></returns>
        private bool Validate(StandParameters standParameters,
            LegParameters legParameters, ScreenParameters screenParameters)
        {
            const double minStandHeight = 10;
            const double maxStandHeight = 20;

            bool checkStandHeight = (standParameters.Height >= minStandHeight) &&
                                    (standParameters.Height <= maxStandHeight);

            if (!checkStandHeight)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorScreenHeight,
                    "The Stand Height are not correct.\n" +
                    "Please observe the folloving relationship:\n" +
                    "10 mm <= Stand Height <= 20 mm");
                return false;
            }

            const double minStandDiameter = 160;
            const double maxStandDiameter = 250;

            bool checkStandDiameter = (standParameters.Diameter >= minStandDiameter) &&
                                      (standParameters.Diameter <= maxStandDiameter);

            if (!checkStandDiameter)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorScreenHeight,
                    "The Stand Diameter are not correct.\n" +
                    "Please observe the folloving relationship:\n" +
                    "160 mm <= Stand Diameter <= 250 mm");
                return false;
            }

            const double minLegHeight = 40;
            const double maxLegHeight = 80;

            bool checkLegHeight = (legParameters.Height >= minLegHeight) &&
                                  (legParameters.Height <= maxLegHeight);

            if (!checkLegHeight)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorScreenHeight,
                    "The Leg Height are not correct.\n" +
                    "Please observe the folloving relationship:\n" +
                    "40 mm <= Leg Height <= 80 mm");
                return false;
            }

            const double minLegWidth = 50;
            const double maxLegWidth = 100;

            bool checkLegWidth = (legParameters.Width >= minLegWidth) &&
                                 (legParameters.Width <= maxLegWidth);

            if (!checkLegWidth)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorScreenHeight,
                    "The Leg Width are not correct.\n" +
                    "Please observe the folloving relationship:\n" +
                    "50 mm <= Leg Width <= 100 mm");
                return false;
            }

            const double minLegThikness = 15;

            bool checkLegThikness = (legParameters.Thikness >= minLegThikness) &&
                                    (legParameters.Thikness <= screenParameters.Thikness);

            if (!checkLegThikness)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorScreenHeight,
                    "The Leg Thikness are not correct.\n" +
                    "Please observe the folloving relationship:\n" +
                    "15 mm <= Leg Thikness <= Screen Thikness");
                return false;
            }

            const double minScreenHeight = 172;
            const double maxScreenHeight = 625;

            bool checkScreenHeight = (screenParameters.Height >= minScreenHeight) &&
                                     (screenParameters.Height <= maxScreenHeight);

            if (!checkScreenHeight)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorScreenHeight,
                    "The Screen Height are not correct.\n" +
                    "Please observe the folloving relationship:\n" +
                    "172 mm <= Screen Height <= 625 mm");
                return false;
            }

            const double minScreenWidth = 400;
            const double maxScreenWidth = 1000;

            bool checkScreenWidth = (screenParameters.Width >= minScreenWidth) &&
                                    (screenParameters.Width <= maxScreenWidth);

            if (!checkScreenWidth)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorScreenHeight,
                    "The Screen Width are not correct.\n" +
                    "Please observe the folloving relationship:\n" +
                    "400 mm <= Screen Width <= 1000 mm");
                return false;
            }

            const double minScreenThikness = 30;
            const double maxScreenThikness = 60;

            bool checkScreenThikness = (screenParameters.Thikness >= minScreenThikness) &&
                                       (screenParameters.Thikness <= maxScreenThikness);

            if (!checkScreenThikness)
            {
                PluginReporter.Instance().Add(
                    PluginReporter.TypeError.ErrorScreenHeight,
                    "The Screen Thikness are not correct.\n" +
                    "Please observe the folloving relationship:\n" +
                    "30 mm <= Screen Thikness <= 60 mm");
                return false;
            }

            return true;
        }

        #endregion

        #region Fields

        /// <summary>
        /// Monitor stand parameters
        /// </summary>
        private StandParameters _standParameters;

        /// <summary>
        /// Monitor leg parameters
        /// </summary>
        private LegParameters _legParameter;

        /// <summary>
        /// Monitor screen parameters
        /// </summary>
        private ScreenParameters _screenParameters;

        #endregion
    }

    /// <summary>
    /// New data type to identify monitor parameters
    /// </summary>
    internal enum ParametersType
    {
        StandHeight = 0,
        StandDiameter,
        LegHeight = 2,
        LegWidth,
        LegThikness,
        ScreenHeight = 5,
        ScreenWidth,
        ScreenThikness
    }
}