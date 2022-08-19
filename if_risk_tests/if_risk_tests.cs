using Microsoft.VisualStudio.TestTools.UnitTesting;
using if_risk;
using FluentAssertions;

namespace IfRiskTests
{
    [TestClass]
    public class RiskTests
    {
        [TestMethod]
        public void GetRiskName()
        {
            var riskName = "Weather hazards";
            var riskPrice = 200;
            var Risk = new Risk(riskName, riskPrice);
            
            string actual = Risk.Name;
            
            Assert.AreEqual(actual, riskName);
        }

        [TestMethod]
        public void GetRiskYearlyPrice()
        {
            var riskName = "Weather hazards";
            var riskPrice = 200;
            var Risk = new Risk(riskName, riskPrice);

            decimal actual = Risk.YearlyPrice;

            Assert.AreEqual(actual, riskPrice);
        }
    }

    [TestClass]
    public class PolicyTests
    {
        [TestMethod]
        public void Policy_Get_NameOfInsuredObject()
        {
            var nameOfInsuredObject = "Car";
            var validFrom = new DateTime(2022, 8, 25);
            var validTill = new DateTime(2023, 4, 25);
            var validMonths = (short)8;
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };
            var Policy = new Policy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var actual = Policy.NameOfInsuredObject;

            Assert.AreEqual(nameOfInsuredObject, actual);
        }

        [TestMethod]
        public void Policy_Get_ValidFrom()
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
            var Policy = new Policy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var actual = Policy.ValidFrom;

            Assert.AreEqual(validFrom, actual);
        }

        [TestMethod]
        public void Policy_Get_ValidTill()
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
            var Policy = new Policy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var expected = new DateTime(2023, 4, 25);
            var actual = Policy.ValidTill;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Policy_Get_ListOfInsuredRisks()
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
            var Policy = new Policy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var actual = Policy.InsuredRisks;

            Assert.AreEqual(listOfRisks, actual);
        }

        [TestMethod]
        public void Policy_Get_Premium()
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
            var Policy = new Policy(nameOfInsuredObject, validFrom, validMonths, listOfRisks);

            var actual = Policy.Premium;
            var expected = 500;

            Assert.AreEqual(expected, actual);
        }
    }
    
    [TestClass]
    public class InsuranceCompanyTests 
    {
        private IInsuranceCompany _insuranceCompany;
        private List<Risk> _listOfRisks;

        [TestInitialize]
        public void Setup()
        {
            var insuranceCompanyName = "If";
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };

            _insuranceCompany = new InsuranceCompany(insuranceCompanyName, listOfRisks);
            _listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };
        }

        [TestMethod]
        public void InsuranceCompany_Get_Name()
        {
            _insuranceCompany.Name.Should().Be("If");
        }

        [TestMethod]
        public void InsuranceCompany_Get_AvailableRisks()
        {
            for(int i = 0; i < _listOfRisks.Count; i++)
            {
                var expected = _listOfRisks[i].Name;
                var actual = _insuranceCompany.AvailableRisks[i].Name;
                Assert.AreEqual(expected, actual);
            }

            for (int i = 0; i < _listOfRisks.Count; i++)
            {
                var expected = _listOfRisks[i].YearlyPrice;
                var actual = _insuranceCompany.AvailableRisks[i].YearlyPrice;
                Assert.AreEqual(expected, actual);
            }

            Assert.AreEqual(_listOfRisks.Count, _insuranceCompany.AvailableRisks.Count);
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


    }
}