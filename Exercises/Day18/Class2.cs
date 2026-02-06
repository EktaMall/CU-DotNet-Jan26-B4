using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3
{
    class Loan
    {
        public string LoanNumber { get; set; }
        public string CustomerName { get; set; }
        public new decimal PrincipalAmount { get; set; }
        public int TenureInYear { get; set; }

        public Loan()
        {
            LoanNumber = string.Empty;
            CustomerName = string.Empty;
            PrincipalAmount = 0;
            TenureInYear = 0;
        }
        public Loan(string loanNumber, string customerName, decimal principalAmount, int tenureInYear)
        {
            LoanNumber = loanNumber;
            CustomerName = customerName;
            PrincipalAmount = principalAmount;
            TenureInYear = tenureInYear;
        }
        public decimal CalculateEMI()
        {
            decimal rate = 10m;
            decimal interest = (PrincipalAmount * rate * TenureInYear) / 100;
            decimal totalAmount = PrincipalAmount + interest;
            int months = TenureInYear * 12;
            return totalAmount / months;
        }
    }
    class HomeLoan : Loan
    {
        public HomeLoan(string loanNumber, string customerName, decimal principalAmount, int tenureInYears)
            : base(loanNumber, customerName, principalAmount, tenureInYears) { }
        public decimal CalculateHomeLoanEMI()
        {
            decimal interest = (PrincipalAmount * 8 * TenureInYear) / 100;
            decimal processingFee = PrincipalAmount * 0.01m;
            decimal totalAmount = PrincipalAmount + interest + processingFee;
            int months = TenureInYear * 12;
            return totalAmount / months;
        }
    }
    class CarLoan : Loan
    {
        public CarLoan(string loanNumber, string customerName, decimal principalAmount, int tenureInYears)
            : base(loanNumber, customerName, principalAmount, tenureInYears)
        {
        }
        public decimal CalculateCarLoanEMI()
        {
            decimal insuranceCharge = 15000m;
            decimal totalPrincipal = PrincipalAmount + insuranceCharge;

            decimal interest = (totalPrincipal * 9 * TenureInYear) / 100;
            decimal totalAmount = totalPrincipal + interest;

            int months = TenureInYear * 12;
            return totalAmount / months;
        }
    }
    internal class Class2
    {
        static void Main(string[] args)
        {
            //Loan loan = new Loan("LN001", "Ekta", 1500000m, 5);
            //Console.WriteLine("Base Loan EMI: " + loan.CalculateEMI());

            //HomeLoan homeLoan = new HomeLoan("HL001", "Kritika", 500000m, 10);
            //Console.WriteLine("Home Loan EMI: " + homeLoan.CalculateHomeLoanEMI());

            //CarLoan carLoan = new CarLoan("CL001", "Neha", 850000m, 5);
            //Console.WriteLine("Car Loan EMI: " + carLoan.CalculateCarLoanEMI());

            Loan[] loans =
            {
            new HomeLoan("101", "Ekta", 500000, 10),
            new HomeLoan("102", "Kritika", 150000, 5),
            new CarLoan("001", "Rahul", 800000, 7),
            new CarLoan("002", "Tushar", 750000, 8),
            };
            for(int i=0; i<loans.Length; i++)
            {
                Console.WriteLine(loans[i].CalculateEMI());
            }
        }
    }
}