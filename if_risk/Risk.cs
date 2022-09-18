using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public class Risk
    {
        public string Name { get; set; }
        public decimal YearlyPrice { get; set; }

        public Risk(string name, decimal yearlyPrice)
        {
            this.Name = name;
            this.YearlyPrice = yearlyPrice;
        }
    }
}
