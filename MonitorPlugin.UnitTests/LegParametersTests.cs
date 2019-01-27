using Monitor_Plugin.Parameters;
using NUnit.Framework;

namespace MonitorPlugin.UnitTests
{
	[TestFixture]
	public class LegParametersTests
	{

		[Test, Description("Positive class constructor test")]
		[TestCase(0, 0, 0, Description = "Pass 0, 0 and 0")]
		[TestCase(-1, -1, -1, Description = "Pass -1, -1 and -1")]
		[TestCase(double.NaN, double.NaN, double.NaN,
			Description = "Pass NaN, NaN and NaN")]
		[TestCase(double.NegativeInfinity, double.NegativeInfinity,
			double.NegativeInfinity,
			Description = "Pass NegativeInfinity," +
			              "NegativeInfinity and NegativeInfinity")]
		[TestCase(double.PositiveInfinity, double.PositiveInfinity,
			double.PositiveInfinity,
			Description = "Pass PositiveInfinity, PositiveInfinity" +
						  " and PositiveInfinity")]
		[TestCase(20, 20, 20, Description = "Pass 20, 20 and 20")]
		[TestCase(25, 25, 25, Description = "Pass 25, 25 and 25")]
		public void PositiveConstructorTest(double height, double width, double thikness)
		{
			LegParameters legParameters = new LegParameters(height, width, thikness);

			Assert.AreEqual(legParameters.Height, height);
			Assert.AreEqual(legParameters.Width, width);
			Assert.AreEqual(legParameters.Thikness, thikness);
		}
	}
}
