using System;
using Monitor_Plugin.Parameters;
using NUnit.Framework;
using System.Collections.Generic;

namespace MonitorPlugin.UnitTests
{
	[TestFixture]
	class MonitorParametersTests
	{
		[Test, Description("Positive test constructor with input values")]
		public void PositiveTestConstructor()
		{
			List<double> monitorParametersList = new List<double>()
			{
				15,
				200,
				50,
				60,
				20,
				330,
				550,
				30
			};

			MonitorParameters monitorParameters = new MonitorParameters(monitorParametersList);

			Assert.AreEqual(monitorParameters.StandParam.Height, 15);
			Assert.AreEqual(monitorParameters.StandParam.Diameter, 200);
			Assert.AreEqual(monitorParameters.LegParam.Height, 50);
			Assert.AreEqual(monitorParameters.LegParam.Width, 60);
			Assert.AreEqual(monitorParameters.LegParam.Thikness, 20);
			Assert.AreEqual(monitorParameters.ScreenParam.Height, 330);
			Assert.AreEqual(monitorParameters.ScreenParam.Width, 550);
			Assert.AreEqual(monitorParameters.ScreenParam.Thikness, 30);
		}

		[Test, Description("Negative test constructor with input values")]
		public void NegativeTestConstructor()
		{
			List<double> monitorParametersList = new List<double>()
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			};

			Assert.Throws<NullReferenceException>(() => { MonitorParameters monitorParameters = new MonitorParameters(monitorParametersList); });
		}
	}
}