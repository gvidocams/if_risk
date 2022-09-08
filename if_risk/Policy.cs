using System;
using System.Collections.Generic;

namespace if_risk
{
    public class Policy : IPolicy
    {
        public string NameOfInsuredObject { get; }
        public DateTime ValidFrom { get; }
        public DateTime ValidTill { get; }
        public IList<Risk> InsuredRisks { get; }
        public Dictionary<Risk, DateTime> RiskPeriods;

        public Policy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> insuredRisks)
        {
            NameOfInsuredObject = nameOfInsuredObject;
            ValidFrom = validFrom;
            ValidTill = validFrom.AddMonths(validMonths); 
            InsuredRisks = insuredRisks;
            RiskPeriods = Helpers.SetRiskPeriods(insuredRisks, validFrom);
        }

        public decimal Premium
        {
            get => Helpers.CalculatePremium(RiskPeriods, ValidTill);
        }
    }
}
