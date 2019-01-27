﻿using Monitor_Plugin.Parameters;
using NUnit.Framework;

namespace MonitorPlugin.UnitTests
{
	[TestFixture]
	public class StandParametersTests
	{

		[Test, Description("Positive class constructor test")]
		[TestCase(0, 0, Description = "Pass 0 and 0")]
		[TestCase(-1, -1, Description = "Pass -1 and -1")]
		[TestCase(double.NaN, double.NaN, Description = "Pass NaN and NaN")]
		[TestCase(double.NegativeInfinity, double.NegativeInfinity,
			Description = "NegativeInfinity and NegativeInfinity")]
		[TestCase(double.PositiveInfinity, double.PositiveInfinity,
			Description = "PositiveInfinity and PositiveInfinity")]
		[TestCase(20, 20, Description = "Pass 20 and 20")]
		[TestCase(25, 25, Description = "Pass 25 and 25")]
		public void PositiveConstructorTest(double height, double diameter)
		{
			StandParameters standParameters = new StandParameters(height, diameter);

			Assert.AreEqual(standParameters.Height, height);
			Assert.AreEqual(standParameters.Diameter, diameter);
		}
	}
}
