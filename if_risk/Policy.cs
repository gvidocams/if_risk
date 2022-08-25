using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public class Policy : IPolicy
    {
        public string NameOfInsuredObject { get; }
        public DateTime ValidFrom { get; }
        public DateTime ValidTill { get; }
        public decimal Premium { get; set; }
        public IList<Risk> InsuredRisks { get; }

        public Policy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> insuredRisks)
        {
            NameOfInsuredObject = nameOfInsuredObject;
            ValidFrom = validFrom;
            ValidTill = validFrom.AddMonths(validMonths); 
            InsuredRisks = insuredRisks;
            Premium = CalculatePremium();
        }

        private decimal CalculatePremium()
        {
            int daysInThisYear = DateTime.IsLeapYear(ValidFrom.Year) ? 366 : 365;

            int validPolicyDays = (ValidTill - ValidFrom).Days;

            decimal premiumSum = 0;

            foreach(Risk risk in InsuredRisks)
            {
                premiumSum += risk.YearlyPrice;
            }

            decimal policyPricePerDay = premiumSum / daysInThisYear;
            decimal policyPrice = policyPricePerDay * validPolicyDays;

            return Math.Round(policyPrice, 2);
        }
    }
}
