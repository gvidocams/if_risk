using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public class InsuranceCompany : IInsuranceCompany
    {
        public string Name { get; }
        public IList<Risk> AvailableRisks { get; set; }
        private IList<Policy> AllPolicies;

        public InsuranceCompany(string name, IList<Risk> availableRisks)
        {
            Name = name;
            AvailableRisks = availableRisks;
            AllPolicies = new List<Policy>();
        }

        public IList<Policy> AllPolicies
        {
            get => AllPolicies;
        }

        public IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks)
        {
            if(validFrom < DateTime.Today)
            {
                throw new Exception("Can't sell a policy in the past!");
            }

            foreach (var selectedRisk in selectedRisks)
            {
                bool isAValidRiskToInsure = false;

                foreach (var availableRisk in AvailableRisks)
                {
                    if (selectedRisk.Name == availableRisk.Name)
                    {
                        isAValidRiskToInsure = true;
                        continue;
                    }
                }

                if(!isAValidRiskToInsure)
                {
                    throw new Exception("This insurance company doesn't insure this risk!");
                }
            }

            var validTill = validFrom.AddMonths(validMonths);

            foreach(var policy in AllPolicies)
            {
                if(policy.NameOfInsuredObject == nameOfInsuredObject)
                {
                    bool isNotAUniqueValidFrom = (validFrom >= policy.ValidFrom) && (validFrom <= policy.ValidTill);
                    bool isNotAUniqueValidTill = (validTill >= policy.ValidFrom) && (validTill <= policy.ValidTill);

                    if (isNotAUniqueValidFrom || isNotAUniqueValidTill)
                    {
                        throw new Exception("Can't sell an already existing policy!");
                    }
                }
            }

            var Policy = new Policy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);
            AllPolicies.Add(Policy);

            return Policy;
        }

        public void AddRisk(string nameOfInsuredObject, Risk Risk, DateTime validFrom)
        {
            if (validFrom < DateTime.Today)
            {
                throw new Exception("Can't add a risk which is set to be valid in the past!");
            }

            foreach(Policy policy in AllPolicies)
            {
                if(policy.NameOfInsuredObject == nameOfInsuredObject)
                {
                    policy.InsuredRisks.Add(Risk);
                    policy.Premium += AddNewlyAddedRiskPremium(Risk, validFrom, policy.ValidTill);

                    return;
                }
            }

            throw new Exception("This object doesn't exist!");
        }

        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            foreach(Policy policy in AllPolicies)
            {
                if(nameOfInsuredObject == policy.NameOfInsuredObject)
                {
                    if(effectiveDate >= policy.ValidFrom && effectiveDate <= policy.ValidTill)
                    {
                        return policy;
                    }
                }
            }

            throw new Exception("No policy found with this date!");
        }

        private decimal AddNewlyAddedRiskPremium(Risk Risk, DateTime validFrom, DateTime validTill)
        {
            int daysInThisYear = DateTime.IsLeapYear(validFrom.Year) ? 366 : 365;

            int validRiskDays = (validTill - validFrom).Days;

            decimal premium = Math.Round(Risk.YearlyPrice / daysInThisYear * validRiskDays, 2);

            return premium;
        }
    }
}
