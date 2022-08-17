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

            var date = GetValidDate();
            Console.WriteLine(date.AddMonths(3));
        }

        static DateTime GetValidDate()
        {
            while (true)
            {
                Console.Write("Enter the year: ");
                var year = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the month: ");
                var month = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the day: ");
                var day = Convert.ToInt32(Console.ReadLine());

                try
                {
                    return new DateTime(year, month, day);
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Not a valid date\n");
                }
            }
            
        }
    }
}
