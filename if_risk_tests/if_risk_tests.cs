using Microsoft.VisualStudio.TestTools.UnitTesting;
using if_risk;

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

            var policyListOfRisks = Policy.InsuredRisks;

            Assert.AreEqual(listOfRisks, policyListOfRisks);
        }
    }
    
    [TestClass]
    public class InsuranceCompanyTests
    {
        [TestMethod]
        public void InsuranceCompany_Get_Name()
        {
            var insuranceCompanyName = "If";
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };
            var InsuranceCompany = new InsuranceCompany(insuranceCompanyName, listOfRisks);

            var actual = InsuranceCompany.Name;

            Assert.AreEqual(insuranceCompanyName, actual);
        }

        [TestMethod]
        public void InsuranceCompany_Get_AvailableRisks()
        {
            var insuranceCompanyName = "If";
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };
            var InsuranceCompany = new InsuranceCompany(insuranceCompanyName, listOfRisks);

            var actual = InsuranceCompany.AvailableRisks;

            Assert.AreEqual(listOfRisks, actual);
        }

        [TestMethod]
        public void InsuranceCompany_Set_AvailableRisks()
        {
            var insuranceCompanyName = "If";
            var listOfRisks = new List<Risk>
            {
                new Risk("Theft", 200),
                new Risk("Weather", 150),
                new Risk("Personal", 400)
            };
            var InsuranceCompany = new InsuranceCompany(insuranceCompanyName, listOfRisks);

            listOfRisks.Add(new Risk("Financial", 2000));
            InsuranceCompany.AvailableRisks = listOfRisks;
            var actual = InsuranceCompany.AvailableRisks;

            Assert.AreEqual(listOfRisks, actual);
        }
    }
}