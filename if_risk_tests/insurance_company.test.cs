using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            _insuranceCompany = new InsuranceCompany("If", _availableListOfRisks, new List<Policy>());
        }

        [TestMethod]
        public void Get_Name_Returns_InsuranceCompany_Name()
        {
            _insuranceCompany.Name.Should().Be("If");
        }

        [TestMethod]
        public void Get_AvailableRisks_Returns_AvailableRisks()
        {
            var expected = _availableListOfRisks;

            var actual = _insuranceCompany.AvailableRisks;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Set_AvailableRisks_Return_New_AvailableRisks()
        {
            var expected = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400),
                new Risk("Financial", 2000)
            };

            _insuranceCompany.AvailableRisks = expected;

            var actual = _insuranceCompany.AvailableRisks;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SellPolicy_SellAPolicyInThePast_Throws_InvalidPolicyStartDateException()
        {
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2002, 8, 26);
            var validMonths = (short)4;

            Assert.ThrowsException<InvalidPolicyStartDateException>(() => _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks));
        }

        [TestMethod]
        public void SellPolicy_SellAValidPolicyAndSellADuplicatePolicy_Throws_DuplicatePolicyException_ValidFromFails()
        {
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 12, 26);
            var validMonths = (short)4;

            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            validFrom = new DateTime(2024, 1, 26);
            validMonths = (short)2;

            Assert.ThrowsException<DuplicatePolicyException>(() => _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks));
        }

        [TestMethod]
        public void SellPolicy_SellAValidPolicyAndSellADuplicatePolicy_Throw_DuplicatePolicyException_ValidTillFails()
        {
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 12, 26);
            var validMonths = (short)4;

            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            validFrom = new DateTime(2023, 10, 26);
            
            Assert.ThrowsException<DuplicatePolicyException>(() => _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks));
        }

        [TestMethod]
        public void SellPolicy_SellTwoIdenticalPolicies_Throws_DuplicatePolicyException()
        {
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 12, 26);
            var validMonths = (short)4;

            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            Assert.ThrowsException<DuplicatePolicyException>(() => _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks));
        }

        [TestMethod]
        public void SellPolicy_SellTwoValidPolicies_Returns_LengthOf_AllPoliciesList_Equals_2()
        {
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2024, 1, 1);
            var validMonths = (short)4;

            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            validFrom = new DateTime(2024, 6, 1);

            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            int expected = 2;
            int actual = _insuranceCompany.AllPolicies.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SellPolicy_SellAPolicy_WithARisk_NotIncludedIn_AvailaleRisks_Throws_NotValidSelectedRisk()
        {
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400),
                new Risk("Earthquake", 500)
            };

            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2024, 1, 1);
            var validMonths = (short)4;

            Exception expected = null;

            
            Assert.ThrowsException<InvalidRiskException>(() => _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks));
            
        }

        [TestMethod]
        public void AddRisk_AddRiskToAValidObject_BeforeItHasBecomeActive()
        {
            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 1, 1);
            var validMonths = (short)3;
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var Risk = new Risk("Global events", 300);

            _insuranceCompany.AddRisk(nameOfInsuredObject, Risk, validFrom);

            var rockPolicy = _insuranceCompany.AllPolicies[0];
            var addedRockRisk = rockPolicy.InsuredRisks.Last();

            Assert.AreEqual(Risk.Name, addedRockRisk.Name);
            Assert.AreEqual(Risk.YearlyPrice, addedRockRisk.YearlyPrice);
        }

        [TestMethod]
        public void AddRisk_AddsARiskToANonExistantObject_Throws_ExceptionIfTheObjectToInsureDoesntExist()
        {
            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 1, 1);
            var Risk = new Risk("Global events", 300);

            Assert.ThrowsException<PolicyNotFoundException>(() => _insuranceCompany.AddRisk(nameOfInsuredObject, Risk, validFrom));
        }

        [TestMethod]
        public void AddRisk_AddsARiskToAValidObject_Returns_PreviousPremiumAndNewlyCalculatedPremium()
        {
            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 1, 1);
            var validMonths = (short)3;
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 100),
                new Risk("Weather", 100),
                new Risk("Personal", 100)
            };
            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var rockPolicy = _insuranceCompany.AllPolicies[0];
            var Risk = new Risk("Global events", 100);
            _insuranceCompany.AddRisk(nameOfInsuredObject, Risk, validFrom);

            decimal expected = (decimal)98.63;

            decimal actual = rockPolicy.Premium;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddRisk_AddsARiskToAValidObject_Throws_InvalidRiskException_Invalid_ValidFrom()
        {
            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 1, 1);
            var validMonths = (short)3;
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var Risk = new Risk("Global events", 300);
            var riskValidFrom = new DateTime(2002, 8, 26);

            Assert.ThrowsException<InvalidRiskException>(() => _insuranceCompany.AddRisk(nameOfInsuredObject, Risk, riskValidFrom));
        }

        [TestMethod]
        public void GetPolicy_GetValidPolicy()
        {
            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 1, 1);
            var validMonths = (short)3;
            var validTill = validFrom.AddMonths(validMonths);
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var effectiveDate = new DateTime(2023, 1, 5);

            var policy = _insuranceCompany.GetPolicy(nameOfInsuredObject, effectiveDate);

            var policyInsuredObject = policy.NameOfInsuredObject;
            var policyValidFrom = policy.ValidFrom;
            var policyValidTill = policy.ValidTill;

            Assert.AreEqual(nameOfInsuredObject, policyInsuredObject);
            Assert.AreEqual(validFrom, policyValidFrom);
            Assert.AreEqual(validTill, policyValidTill);
        }

        [TestMethod]
        public void GetPolicy_GetANonExistantPolicy_Throws_NotExistingPolicy()
        {
            var nameOfInsuredObject = "Rock";
            var effectiveDate = new DateTime(2023, 1, 5);

            Exception expected = null;

            Assert.ThrowsException<PolicyNotFoundException>(() => _insuranceCompany.GetPolicy(nameOfInsuredObject, effectiveDate));
        }
    }
}