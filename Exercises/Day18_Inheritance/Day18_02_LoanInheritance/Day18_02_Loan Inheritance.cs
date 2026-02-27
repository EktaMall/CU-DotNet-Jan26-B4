using System;

namespace Week3
{
    class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal BasicSalary { get; set; }
        public int ExperienceInYears { get; set; }

        public Employee(int id, string name, decimal salary, int experience)
        {
            EmployeeId = id;
            EmployeeName = name;
            BasicSalary = salary;
            ExperienceInYears = experience;
        }

        public decimal CalculateAnnualSalary()
        {
            return BasicSalary * 12;
        }

        public void DisplayEmployeeDetails(decimal annualSalary)
        {
            Console.WriteLine($"ID: {EmployeeId}");
            Console.WriteLine($"Name: {EmployeeName}");
            Console.WriteLine($"Annual Salary: {annualSalary}");
            Console.WriteLine();
        }
    }

    class PermanentEmployee : Employee
    {
        public PermanentEmployee(int id, string name, decimal salary, int experience)
            : base(id, name, salary, experience) { }

        public new decimal CalculateAnnualSalary()
        {
            decimal hra = BasicSalary * 0.20m;
            decimal sa = BasicSalary * 0.10m;
            decimal bonus = ExperienceInYears >= 5 ? 50000 : 0;

            decimal monthly = BasicSalary + hra + sa;
            return (monthly * 12) + bonus;
        }
    }

    class ContractEmployee : Employee
    {
        public int ContractDurationInMonths { get; set; }

        public ContractEmployee(int id, string name, decimal salary, int experience, int months)
            : base(id, name, salary, experience)
        {
            ContractDurationInMonths = months;
        }

        public new decimal CalculateAnnualSalary()
        {
            decimal bonus = ContractDurationInMonths >= 12 ? 30000 : 0;
            return (BasicSalary * 12) + bonus;
        }
    }

    class InternEmployee : Employee
    {
        public InternEmployee(int id, string name, decimal stipend, int experience)
            : base(id, name, stipend, experience) { }

        public new decimal CalculateAnnualSalary()
        {
            return BasicSalary * 12;
        }
    }

    internal class Day18_02
    {
        static void Main(string[] args)
        {
            Employee e1 = new Employee(1, "Ekta", 50000, 3);

            PermanentEmployee e2 = new PermanentEmployee(1, "Kritika", 40000, 6);

            ContractEmployee e3 = new ContractEmployee(2, "Kushagra", 35000, 2, 14);

            InternEmployee e4  = new InternEmployee(3, "Tushar", 40000, 6);

            e1.DisplayEmployeeDetails(e1.CalculateAnnualSalary());
            e2.DisplayEmployeeDetails(e2.CalculateAnnualSalary());
            e3.DisplayEmployeeDetails(e3.CalculateAnnualSalary());
            e4.DisplayEmployeeDetails(e4.CalculateAnnualSalary());
        }
    }
}
