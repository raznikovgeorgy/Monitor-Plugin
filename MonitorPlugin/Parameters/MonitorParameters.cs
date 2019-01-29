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
        /// Monitor parameter parameters
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
			return (CheckParameter(10, 20, standParameters.Height,
				PluginReporter.TypeError.ErrorStandHeight, "Stand Height") &&

				CheckParameter(160, 250, standParameters.Diameter,
				PluginReporter.TypeError.ErrorStandDiameter, "Stand Diameter") &&

				CheckParameter(40, 80, legParameters.Height,
				PluginReporter.TypeError.ErrorLegHeight, "Leg Height") &&

				CheckParameter(50, 100, legParameters.Width,
				PluginReporter.TypeError.ErrorLegWidth, "Leg Width") &&

				CheckParameter(15, screenParameters.Thikness, legParameters.Thikness,
				PluginReporter.TypeError.ErrorLegThikness, "Leg Thikness") &&

				CheckParameter(172, 625, screenParameters.Height,
				PluginReporter.TypeError.ErrorScreenHeight, "Screen Height") &&

				CheckParameter(400, 1000, screenParameters.Width,
				PluginReporter.TypeError.ErrorScreenWidth, "Screen Width") &&

				CheckParameter(30, 60, screenParameters.Thikness,
				PluginReporter.TypeError.ErrorScreenThikness, "Screens Thikness"));
        }

		/// <summary>
		/// Check parameter valid values
		/// </summary>
		/// <param name="minValue">Minimal value</param>
		/// <param name="maxValue">Maximum value</param>
		/// <param name="parameter">Parameter</param>
		/// <param name="error">Type of error</param>
		/// <param name="parameterName">Name of the parameter</param>
		/// <returns></returns>
		private bool CheckParameter(double minValue, double maxValue, double parameter,
			PluginReporter.TypeError error, string parameterName)
		{
			if ((parameter >= minValue) &&
				(parameter <= maxValue) &&
				!double.IsNaN(parameter) &&
				!double.IsInfinity(parameter) &&
				!double.IsNegativeInfinity(parameter) &&
				!double.IsPositiveInfinity(parameter))
			{
				return true;
			}

			PluginReporter.Instance().Add(error, $"{parameterName} - value aren't correct\n" +
				$"Please observe the folloving relationship:\n " +
				$"{minValue} <= {parameterName} <= {maxValue}.");

			return false;
		}


		#endregion

		#region Fields

		/// <summary>
		/// Monitor parameter parameters
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