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

            foreach (Policy policy in AllPolicies)
            {
                if(policy.NameOfInsuredObject == nameOfInsuredObject)
                {
                    policy.RiskPeriods.Add(Risk, validFrom);
                    return;
                }
            }

            throw new PolicyNotFoundException("This object doesn't exist!");
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

            throw new PolicyNotFoundException("No policy found with this date!");
        }
    }
}
