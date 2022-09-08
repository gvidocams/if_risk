using System;
using System.Collections.Generic;

namespace if_risk
{
    public class InsuranceCompany : IInsuranceCompany
    {
        public string Name { get; }
        public IList<Risk> AvailableRisks { get; set; }
        public IList<Policy> AllPolicies { get; }

        public InsuranceCompany(string name, IList<Risk> availableRisks, IList<Policy> allPolicies)
        {
            Name = name;
            AvailableRisks = availableRisks;
            AllPolicies = allPolicies;
        }

        public IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks)
        {
            Helpers.IsValidFromInThePast(validFrom);

            Helpers.CheckIfRisksAreValid(selectedRisks, AvailableRisks);

            var validTill = validFrom.AddMonths(validMonths);

            Helpers.IsAValidPolicyToInsure(nameOfInsuredObject, AllPolicies, validFrom, validTill);

            var Policy = new Policy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);

            AllPolicies.Add(Policy);

            return Policy;
        }

        public void AddRisk(string nameOfInsuredObject, Risk Risk, DateTime validFrom)
        {
            Helpers.IsValidFromInThePast(validFrom);

            Helpers.FindPolicy(AllPolicies, nameOfInsuredObject).RiskPeriods.Add(Risk, validFrom);
        }

        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            var policy = Helpers.FindPolicy(AllPolicies, nameOfInsuredObject);

            if (effectiveDate >= policy.ValidFrom && effectiveDate <= policy.ValidTill)
            {
                return policy;
            }

            throw new InvalidDateException();
        }
    }
}
