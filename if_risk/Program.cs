using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace if_risk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listOfRisks = new List<Risk>();
            
            listOfRisks.Add(new Risk("Weather hazards", 100));
            listOfRisks.Add(new Risk("Safety hazards", 150));
            listOfRisks.Add(new Risk("Theft chances", 50));

            var InsuranceCompany = new InsuranceCompany("If", listOfRisks);

            var exception = new Exception("Hello");
            var exception2 = "Hello";

            Console.WriteLine(exception.Message == exception2);
        }

    }
}
