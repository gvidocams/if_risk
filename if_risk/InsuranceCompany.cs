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

        public InsuranceCompany(string name, IList<Risk> availableRisks)
        {
            _name = name;
            _availableRisks = availableRisks;
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
            return new Policy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);
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
