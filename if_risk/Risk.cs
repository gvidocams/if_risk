using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace if_risk
{
    public class Risk
    {
        public string name;
        public decimal yearlyPrice;

        public Risk(string name, decimal yearlyPrice)
        {
            this.name = name;
            this.yearlyPrice = yearlyPrice;
        }

        public string Name 
        { 
            get => name; 
            set => name = value; 
        }

        public decimal YearlyPrice { 
            get => yearlyPrice;
            set => yearlyPrice = value; 
        }
    }
}
