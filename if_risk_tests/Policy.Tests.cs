using if_risk;
using FluentAssertions;

namespace IfRiskTests
{
    [TestClass]
    public class PolicyTests
    {
        private IPolicy _policy;
        private List<Risk> _listOfRisks;

        [TestInitialize]
        public void Setup()
        {
            _listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };
            _policy = new Policy("Car", new DateTime(2022, 8, 25), (short)8, _listOfRisks);
        }

        [TestMethod]
        public void Get_NameOfInsuredObject_Returns_NameOfInsuredObject()
        {
            _policy.NameOfInsuredObject.Should().Be("Car");
        }

        [TestMethod]
        public void Get_ValidFrom_Returns_ValidFrom()
        {
            _policy.ValidFrom.Should().Be(new DateTime(2022, 8, 25));
        }

        [TestMethod]
        public void Get_ValidTill_Returns_ValidTill()
        {
            _policy.ValidTill.Should().Be(new DateTime(2023, 4, 25));
        }

        [TestMethod]
        public void Get_ListOfInsuredRisks_Returns_ListOfInsuredRisks()
        {
            _policy.InsuredRisks.Should().BeEquivalentTo(_listOfRisks);
        }

        [TestMethod]
        public void Get_Premium_ReturnsPremium()
        {
            _policy.Premium.Should().Be(499.32m);
        }
    }
}