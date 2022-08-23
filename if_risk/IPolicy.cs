using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public interface IPolicy
    {
        string NameOfInsuredObject { get; }

        DateTime ValidFrom { get; }

        DateTime ValidTill { get; }

        decimal Premium { get; set; }

        IList<Risk> InsuredRisks { get; }
    }
}
