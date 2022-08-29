using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public class Helpers
    {
        public static Dictionary<Risk, DateTime> SetRiskPeriods(IList<Risk> listOfRisks, DateTime validFrom)
        {
            Dictionary<Risk, DateTime> riskPeriods = new Dictionary<Risk, DateTime>();

            foreach (Risk risk in listOfRisks)
            {
                riskPeriods.Add(risk, validFrom);
            }

            return riskPeriods;
        }

        public static decimal CalculatePremium(Dictionary<Risk, DateTime> riskPeriods, DateTime validTill)
        {
            int daysInThisYear = DateTime.IsLeapYear(validTill.Year) ? 366 : 365;

            decimal premium = 0;

            foreach(KeyValuePair<Risk, DateTime> entry in riskPeriods)
            {
                int policyDays = (validTill - entry.Value).Days;

                decimal priceToInsureThisRisk = entry.Key.YearlyPrice / daysInThisYear * policyDays;

                premium += priceToInsureThisRisk;
            }

            return Math.Round(premium, 2);
        }

        public static void IsAValidPolicyToInsure(string nameOfInsuredObject, IList<Policy> allPolicies, DateTime validFrom, DateTime validTill)
        {
            foreach (Policy policy in allPolicies)
            {
                if (policy.NameOfInsuredObject == nameOfInsuredObject)
                {
                    bool isNotAUniqueValidFrom = (validFrom >= policy.ValidFrom) && (validFrom <= policy.ValidTill);
                    bool isNotAUniqueValidTill = (validTill >= policy.ValidFrom) && (validTill <= policy.ValidTill);

                    if (isNotAUniqueValidFrom || isNotAUniqueValidTill)
                    {
                        throw new DuplicatePolicyException("Can't sell an already existing policy!");
                    }
                }
            }
        }

        public static void IsValidFromInThePast(DateTime validFrom)
        {
            if (validFrom < DateTime.Today)
            {
                throw new InvalidDateException("Valid from date can't be in the past!");
            }
        }

        public static void CheckIfRisksAreValid(IList<Risk> selectedRisks, IList<Risk> availableRisks)
        {
            foreach (var selectedRisk in selectedRisks)
            {
                bool isAValidRiskToInsure = false;

                foreach (var availableRisk in availableRisks)
                {
                    if (selectedRisk.Name == availableRisk.Name)
                    {
                        isAValidRiskToInsure = true;
                        continue;
                    }
                }

                if (!isAValidRiskToInsure)
                {
                    throw new InvalidRiskException("This insurance company doesn't insure this risk!");
                }
            }
        }
    }
}
