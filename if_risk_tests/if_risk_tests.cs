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
            var riskName = "Weather hazards";
            var riskPrice = 200;

            _risk = new Risk(riskName, riskPrice);
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

    [TestClass]
    public class PolicyTests
    {
        private IPolicy _policy;
        private List<Risk> _listOfRisks;

        [TestInitialize]
        public void Setup()
        {
            var nameOfInsuredObject = "Car";
            var validFrom = new DateTime(2022, 8, 25);
            var validMonths = (short)8;
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };
            _listOfRisks = listOfRisks;
            _policy = new Policy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

        }

        [TestMethod]
        public void Policy_Get_NameOfInsuredObject()
        {
            var expected = "Car";

            var actual = _policy.NameOfInsuredObject;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Policy_Get_ValidFrom()
        {
            var expected = new DateTime(2022, 8, 25);
            
            var actual = _policy.ValidFrom;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Policy_Get_ValidTill()
        {
            var expected = new DateTime(2023, 4, 25);

            var actual = _policy.ValidTill;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Policy_Get_ListOfInsuredRisks()
        {
            var expected = _listOfRisks;

            var actual = _policy.InsuredRisks;
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Policy_Get_Premium()
        {
            var actual = _policy.Premium;

            decimal expected = (decimal)499.32;

            Assert.AreEqual(expected, actual);
        }
    }
    
    [TestClass]
    public class InsuranceCompanyTests 
    {
        private IInsuranceCompany _insuranceCompany;
        private List<Risk> _availableListOfRisks;

        [TestInitialize]
        public void Setup()
        {
            var insuranceCompanyName = "If";
            _availableListOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            _insuranceCompany = new InsuranceCompany(insuranceCompanyName, _availableListOfRisks);
        }

        [TestMethod]
        public void InsuranceCompany_Get_Name()
        {
            _insuranceCompany.Name.Should().Be("If");
        }

        [TestMethod]
        public void InsuranceCompany_Get_AvailableRisks()
        {
            var expected = _availableListOfRisks;

            var actual = _insuranceCompany.AvailableRisks;

            Assert.AreEqual(expected, actual);
        }
       
        [TestMethod]
        public void InsuranceCompany_Set_AvailableRisks()
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
        public void InsuranceCompany_SellPolicy_Catch_Invalid_ValidFrom()
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

            Exception expected = null;

            try
            {
                _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);
            }
            catch (Exception ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void InsuranceCompany_SellPolicy_Catch_AlreadyExistingPolicy_ValidFrom()
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

            Exception expected = null;

            try
            {
                _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);
            }
            catch (Exception ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void InsuranceCompany_SellPolicy_Catch_AlreadyExistingPolicy_ValidTill()
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

            Exception expected = null;

            try
            {
                _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);
            }
            catch (Exception ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void InsuranceCompany_SellPolicy_Catch_AlreadyExistingPolicy_Identical()
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

            Exception expected = null;

            try
            {
                _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);
            }
            catch (Exception ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void InsuranceCompany_SellPolicy_SellTwoValidPolicies_IdenticalObjectToInsure()
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

            Exception expected = null;

            try
            {
                _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);
            }
            catch(Exception ex)
            {
                expected = ex;
            }

            Assert.IsNull(expected);
        }

        [TestMethod]
        public void InsuranceCompany_SellPolicy_Catch_NotValidSelectedRisk()
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

            try
            {
                _insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);
            }
            catch(Exception ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void InsuranceCompany_AddRisk_AddRiskToAValidObject_BeforeItHasBecomeActive()
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
        public void InsuranceCompany_AddRisk_Catch_ExceptionIfTheObjectToInsureDoesntExist()
        {
            var nameOfInsuredObject = "Rock";
            var validFrom = new DateTime(2023, 1, 1);
            var Risk = new Risk("Global events", 300);

            Exception expected = null;

            try
            {
                _insuranceCompany.AddRisk(nameOfInsuredObject, Risk, validFrom);
            }
            catch(Exception ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void InsuranceCompany_AddRisk_AddRiskToAValidObject_AddPremium()
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
        public void InsuranceCompany_AddRisk_Catch_RiskValidFrom_InThePast()
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

            Exception expected = null;

            try
            {
                _insuranceCompany.AddRisk(nameOfInsuredObject, Risk, riskValidFrom);
            }
            catch(Exception ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void InsuranceCompany_GetPolicy_GetValidPolicy()
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
        public void InsuranceCompany_GetPolicy_Catch_NotExistingPolicy()
        {
            var nameOfInsuredObject = "Rock";
            var effectiveDate = new DateTime(2023, 1, 5);

            Exception expected = null;

            try
            {
                var policy = _insuranceCompany.GetPolicy(nameOfInsuredObject, effectiveDate);
            }
            catch(Exception ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }
    }
}