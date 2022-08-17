using Microsoft.VisualStudio.TestTools.UnitTesting;
using if_risk;

namespace IfRiskTests
{
    [TestClass]
    public class if_risk_tests
    {
        [TestMethod]
        public void GetRiskNameAndYearlyPrice()
        {
            var riskName = "Weather hazards";
            var riskPrice = 200;
            var Risk = new Risk(riskName, riskPrice);

            
            string riskNameResult = Risk.Name;
            decimal riskPriceResult = Risk.YearlyPrice;
            
            Assert.AreEqual(riskNameResult, riskName);
            Assert.AreEqual(riskPriceResult, riskPrice);
        }
    }
}