using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public interface IInsuranceCompany
    {
        string Name { get; }

        IList<Risk> AvailableRisks { get; set; }
        IList<Policy> AllPolicies { get; }
        IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks);
        /// <summary>
        /// Add risk to the policy of insured object.
        /// </summary>
        /// <param name="nameOfInsuredObject">Name of insured object</param>
        /// <param name="risk">Risk that must be added</param>
        /// <param name="validFrom">Date when risk becomes active. Can not be in the past</param>
        void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom);
        IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate);
    }
}
