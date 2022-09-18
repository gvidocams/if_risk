using if_risk;
using FluentAssertions;

namespace IfRiskTests
{
    [TestClass]
    public class InsuranceCompanyTests
    {
        private IInsuranceCompany _insuranceCompany;
        private List<Risk> _availableListOfRisks;

        [TestInitialize]
        public void Setup()
        {
            _availableListOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            var policy = new Policy("Rock", new DateTime(2023, 1, 1), (short)3, _availableListOfRisks);

            _insuranceCompany = new InsuranceCompany("If", _availableListOfRisks, new List<Policy> { policy });
        }

        [TestMethod]
        public void Get_Name_Returns_InsuranceCompany_Name()
        {
            _insuranceCompany.Name.Should().Be("If");
        }

        [TestMethod]
        public void Get_AvailableRisks_Returns_AvailableRisks()
        {
            _insuranceCompany.AvailableRisks.Should().BeEquivalentTo(_availableListOfRisks);
        }

        [TestMethod]
        public void Set_AvailableRisks_Return_NewAvailableRisks()
        {
            _availableListOfRisks.Add(new Risk("Financial", 2000));

            _insuranceCompany.AvailableRisks = _availableListOfRisks;

            _insuranceCompany.AvailableRisks.Should().BeEquivalentTo(_availableListOfRisks);
        }

        [TestMethod]
        public void SellPolicy_SellAPolicyInThePast_ThrowsInvalidPolicyStartDateException()
        {
            Action act = () => 
                _insuranceCompany.SellPolicy("Rock", new DateTime(2002, 8, 26), (short)4, _availableListOfRisks);

            act.Should().Throw<InvalidDateException>()
                .WithMessage("Can't set a date in the past!");
        }

        [TestMethod]
        public void SellPolicy_SellAValidPolicyAndSellADuplicatePolicy_ThrowsDuplicatePolicyException_ValidFromFails()
        {
            Action act = () =>
                _insuranceCompany.SellPolicy("Rock", new DateTime(2023, 2, 1), (short)4, _availableListOfRisks);

            act.Should().Throw<DuplicatePolicyException>()
                .WithMessage("Can't sell an already existing policy!");
        }

        [TestMethod]
        public void SellPolicy_SellAValidPolicyAndSellADuplicatePolicy_ThrowsDuplicatePolicyException_ValidTillFails()
        {
            Action act = () =>
                _insuranceCompany.SellPolicy("Rock", new DateTime(2022, 12, 26), (short)3, _availableListOfRisks);

            act.Should().Throw<DuplicatePolicyException>()
                .WithMessage("Can't sell an already existing policy!");
        }

        [TestMethod]
        public void SellPolicy_SellTwoIdenticalPolicies_ThrowsDuplicatePolicyException()
        {
            Action act = () =>
                _insuranceCompany.SellPolicy("Rock", new DateTime(2023, 1, 1), (short)3, _availableListOfRisks);

            act.Should().Throw<DuplicatePolicyException>()
                .WithMessage("Can't sell an already existing policy!");
        }

        [TestMethod]
        public void SellPolicy_SellTwoValidPolicies_ReturnsLengthOfAllPoliciesList_Equals_3()
        {
            _insuranceCompany.SellPolicy("Rock", new DateTime(2024, 1, 1), (short)4, _availableListOfRisks);

            _insuranceCompany.SellPolicy("Rock", new DateTime(2024, 6, 1), (short)4, _availableListOfRisks);

            _insuranceCompany.AllPolicies.Count.Should().Be(3);
        }

        [TestMethod]
        public void SellPolicy_SellAPolicyWithARiskNotIncludedInAvailaleRisks_ThrowsInvalidRiskException()
        {
            var listOfRisks = _availableListOfRisks.ToList();
            listOfRisks.Add(new Risk("Earthquake", 500));

            Action act = () =>
                _insuranceCompany.SellPolicy("Rock", new DateTime(2024, 1, 1), (short)4, listOfRisks);

            act.Should().Throw<InvalidRiskException>()
                .WithMessage("This insurance company doesn't insure this risk!");
        }

        [TestMethod]
        public void AddRisk_AddRiskToAValidObject_BeforeItHasBecomeActive()
        {
            var Risk = new Risk("Global events", 300);

            _insuranceCompany.AddRisk("Rock", Risk, new DateTime(2023, 1, 1));

            var rockPolicy = _insuranceCompany.AllPolicies[0];
            var addedRockRisk = rockPolicy.RiskPeriods.Last().Key;

            addedRockRisk.Should().BeEquivalentTo(Risk);
        }

        [TestMethod]
        public void AddRisk_AddsARiskToANonExistantObject_ThrowsPolicyNotFoundException()
        {
            Action act = () =>
                _insuranceCompany.AddRisk("House", new Risk("Global events", 300), new DateTime(2023, 1, 1));

            act.Should().Throw<PolicyNotFoundException>()
                .WithMessage("Policy with this insured object doesn't exist!");
        }

        [TestMethod]
        public void AddRisk_AddsARiskToAValidObject_Returns_PreviousPremiumAndNewlyCalculatedPremium()
        {
            var rockPolicy = _insuranceCompany.AllPolicies[0];
            var Risk = new Risk("Global events", 100);

            _insuranceCompany.AddRisk("Rock", Risk, new DateTime(2023, 1, 1));

            rockPolicy.Premium.Should().Be(209.59m);
        }

        [TestMethod]
        public void AddRisk_AddsARiskToAValidObject_Throws_InvalidRiskException_Invalid_ValidFrom()
        {
            var Risk = new Risk("Global events", 300);

            Action act = () =>
                _insuranceCompany.AddRisk("Rock", Risk, new DateTime(2002, 8, 26));

            act.Should().Throw<InvalidDateException>()
                .WithMessage("Can't set a date in the past!");
        }

        [TestMethod]
        public void GetPolicy_GetValidPolicy()
        {
            var policy = _insuranceCompany.GetPolicy("Rock", new DateTime(2023, 1, 5));

            policy.Should().BeEquivalentTo(new Policy("Rock", new DateTime(2023, 1, 1), (short)3, _availableListOfRisks));
        }

        [TestMethod]
        public void GetPolicy_GetANonExistantPolicy_Throws_NotExistingPolicy()
        {
            Action act = () => 
                _insuranceCompany.GetPolicy("House", new DateTime(2023, 1, 5));

            act.Should().Throw<PolicyNotFoundException>()
                .WithMessage("Policy with this insured object doesn't exist!");
        }
    }
}