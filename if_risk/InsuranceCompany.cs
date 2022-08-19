using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public class InsuranceCompany : IInsuranceCompany
    {
        private string _name;
        private IList<Risk> _availableRisks;
        private IList<Policy> _allPolicies;

        public InsuranceCompany(string name, IList<Risk> availableRisks)
        {
            _name = name;
            _availableRisks = availableRisks;
            _allPolicies = new List<Policy>();
        }

        public string Name {
            get => _name;
        }

        public IList<Risk> AvailableRisks
        {
            get => _availableRisks;
            set => _availableRisks = value;
        }

        public IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks)
        {
            if(validFrom < DateTime.Today)
            {
                throw new Exception("Can't sell a policy in the past!");
            }

            var validTill = validFrom.AddMonths(validMonths);

            foreach(var policy in _allPolicies)
            {
                if(policy.NameOfInsuredObject == nameOfInsuredObject)
                {
                    if((validFrom >= policy.ValidFrom && validFrom <= policy.ValidTill) || (validTill >= policy.ValidFrom && validTill <= policy.ValidTill))
                    {
                        throw new Exception("Can't sell an already existing policy!");
                    }
                }
            }

            var Policy = new Policy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);
            _allPolicies.Add(Policy);

            return Policy;
        }

        public void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom)
        {
            throw new NotImplementedException();
        }

        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            throw new NotImplementedException();
        }
    }
}
