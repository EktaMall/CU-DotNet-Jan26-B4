using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Week8_Assessment
{
    public class EmployeeBonus
    {
        public decimal BaseSalary { get; set; }
        public int PerformanceRating { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal DepartmentMultiplier { get; set; }
        public double AttendancePercentage { get; set; }

        public decimal NetAnnualBonus
        {
            get
            {
                if (BaseSalary <= 0)
                    return 0m;

                if (PerformanceRating < 1 || PerformanceRating > 5)
                    throw new InvalidOperationException("Invalid Performance Rating");

                if (AttendancePercentage < 0 || AttendancePercentage > 100)
                    throw new InvalidOperationException("Attendance must be between 0 and 100.");
                decimal bonusPercentage = 0m;

                switch (PerformanceRating)
                {
                    case 5: bonusPercentage = 0.25m; break;
                    case 4: bonusPercentage = 0.18m; break;
                    case 3: bonusPercentage = 0.12m; break;
                    case 2: bonusPercentage = 0.05m; break;
                    case 1: bonusPercentage = 0m; break;
                }

                if (YearsOfExperience > 10)
                    bonusPercentage += 0.05m;
                else if (YearsOfExperience > 5)
                    bonusPercentage += 0.03m;

                decimal bonus = BaseSalary * bonusPercentage;

                if (AttendancePercentage < 85)
                    bonus *= 0.80m;

                bonus *= DepartmentMultiplier;

                decimal Cap = BaseSalary * 0.40m;
                if (bonus > Cap)
                    bonus = Cap;

                decimal taxRate = 0m;

                if (bonus <= 150000)
                    taxRate = 0.10m;
                else if (bonus <= 300000)
                    taxRate = 0.20m;
                else
                    taxRate = 0.30m;

                decimal finalBonus = bonus - (bonus * taxRate);

                return Math.Round(finalBonus, 2);
            }
        }
    }
}