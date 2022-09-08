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
        public void Get_RiskName_Returns_RiskName()
        {
            _risk.Name.Should().Be("Weather hazards");
        }

        [TestMethod]
        public void Get_RiskYearlyPrice_Returns_RiskYearlyPrice()
        {
            _risk.YearlyPrice.Should().Be(200);
        }
    }
}