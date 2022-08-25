﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var expected = "Car";

            var actual = _policy.NameOfInsuredObject;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_ValidFrom_Returns_ValidFrom()
        {
            var expected = new DateTime(2022, 8, 25);

            var actual = _policy.ValidFrom;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_ValidTill_Returns_ValidTill()
        {
            var expected = new DateTime(2023, 4, 25);

            var actual = _policy.ValidTill;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_ListOfInsuredRisks_Returns_ListOfInsuredRisks()
        {
            var expected = _listOfRisks;

            var actual = _policy.InsuredRisks;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Premium_ReturnsPremium()
        {
            var actual = _policy.Premium;

            decimal expected = (decimal)499.32;

            Assert.AreEqual(expected, actual);
        }
    }
}