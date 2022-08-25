using Microsoft.VisualStudio.TestTools.UnitTesting;
using if_risk;
using FluentAssertions;

namespace IfRiskTests
{
    [TestClass]
    public class RiskTests
    {
        private Risk _risk;

        [TestInitialize]
        public void Setup()
        {
            _risk = new Risk("Weather hazards", 200);
        }

        [TestMethod]
        public void GetRiskName()
        {
            var expected = "Weather hazards";

            var actual = _risk.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRiskYearlyPrice()
        {
            var expected = 200;

            var actual = _risk.YearlyPrice;

            Assert.AreEqual(expected, actual);
        }
    }
}