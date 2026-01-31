using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Week4
{

    class Patient
    {
        public string Name { get; set; }

        public decimal BaseFee { get; set; }

        public Patient (string name, decimal baseFee)
        {
            Name = name;
            BaseFee = baseFee;
        }

        public virtual decimal CalculateFinalBill()
        {
            return BaseFee;
        }
    }

    class Inpatient : Patient
    {
        public int DaysStayed { get; set; }

        public decimal DailyRate { get; set; }

        public Inpatient(string name, decimal baseFee, int daysStayed, decimal dailyRate) : base(name, baseFee)
        {
            DaysStayed = daysStayed;
            DailyRate = dailyRate;
        }

        public override decimal CalculateFinalBill()
        {
            return BaseFee + (DaysStayed * DailyRate);
        }
    }

    class Outpatient : Patient
    {
        public decimal ProcedureFee { get; set; }

        public Outpatient(string name, decimal baseFee, decimal procedureFee) : base(name, baseFee)
        {
            ProcedureFee = procedureFee;
        }
        public override decimal CalculateFinalBill()
        {
            return BaseFee + ProcedureFee;
        }
    }

    class EmergencyPatient : Patient
    {
        public int SeverityLevel { get; set; }

        public EmergencyPatient(string name, decimal baseFee, int severityLevel) : base(name, baseFee)
        {
            if (severityLevel < 1 || severityLevel > 5)
            {
                throw new ArgumentException("SeverityLevel must be between 1 and 5");
            }

            SeverityLevel = severityLevel;
        }
        public override decimal CalculateFinalBill()
        {
            return BaseFee * SeverityLevel;
        }
    }

    class HospitalBilling
    {
        private List<Patient> patients = new List<Patient>();

        public void AddPatient(Patient p)
        {
            patients.Add(p);
        }

        public void GenerateDailyReport()
        {
            Console.WriteLine("Daily Billing Report");
            foreach (Patient p in patients)
            {
                decimal bill = p.CalculateFinalBill();
                Console.WriteLine($"{p.Name} : {bill.ToString("C2")}");
            }
        }

        public decimal CalculateTotalRevenue()
        {
            decimal total = 0;
            foreach (Patient p in patients)
            {
                total += p.CalculateFinalBill();
            }
            return total;
        }

        public int GetInpatientCount()
        {
            int count = 0;
            foreach (Patient p in patients)
            {
                if (p is Inpatient)
                    count++;
            }
            return count;
        }
    }
    internal class Week4Assessment
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            HospitalBilling billing = new HospitalBilling();

            billing.AddPatient(new Inpatient("Ekta", 2000m, 3, 1500m));
            billing.AddPatient(new Outpatient("Kritika", 1000m, 2500m));
            billing.AddPatient(new EmergencyPatient("Aman", 3000m, 4));
            billing.AddPatient(new Inpatient("Ravi", 1800m, 2, 1200m));

            billing.GenerateDailyReport();

            Console.WriteLine("--------------------------------");
            Console.WriteLine("Total Revenue:" + billing.CalculateTotalRevenue().ToString("C2"));
            Console.WriteLine($"Inpatient Count: {billing.GetInpatientCount()}");
        }
    }
}
