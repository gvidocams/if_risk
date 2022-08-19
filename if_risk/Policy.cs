﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public class Policy : IPolicy
    {
        private string _nameOfInsuredObject;
        private DateTime _validFrom;
        private DateTime _validTill;
        private short _validMonths;
        private decimal _premium;
        private IList<Risk> _insuredRisks;

        public Policy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> insuredRisks)
        {
            _nameOfInsuredObject = nameOfInsuredObject;
            _validFrom = validFrom;
            _validMonths = validMonths;
            _validTill = validFrom.AddMonths(validMonths); 
            _insuredRisks = insuredRisks;
            _premium = CalculatePremium();
        }

        public string NameOfInsuredObject
        {
            get => _nameOfInsuredObject;
        }

        public DateTime ValidFrom
        {
            get => _validFrom;
        }

        public DateTime ValidTill
        {
            get => _validTill;
        }

        public decimal Premium
        {
            get => _premium;
        }

        public IList<Risk> InsuredRisks 
        {
            get => _insuredRisks;
        }

        private decimal CalculatePremium()
        {
            decimal premiumSum = 0;

            foreach(Risk risk in _insuredRisks)
            {
                premiumSum += risk.YearlyPrice;
            }

            decimal policyPricePerMonth = premiumSum / 12;
            decimal policyPrice = policyPricePerMonth * _validMonths;

            return Math.Round(policyPrice, 2);
        }
    }
}
