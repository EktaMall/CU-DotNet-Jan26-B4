using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PortfolioMAnagementApp
{


    interface IRiskAssessable
    {
        string GetRiskCategory();
    }

    interface IReportable
    {
        string GenerateReportLine();
    }

    class InvalidFinancialDataException : Exception
    {
        public InvalidFinancialDataException(string message) : base(message) { }
    }

    abstract class FinancialInstrument
    {
        private int quantity;
        private decimal purchasePrice;
        private decimal marketPrice;
        private string currency;

        public string InstrumentID { get; set; }
        public string Name { get; set; }
        public DateOnly PurchaseDate { get; set; }

        public string Currency
        {
            get { return currency; }
            set
            {
                if (value.Length != 3) throw new InvalidFinancialDataException("Invalid Currency");
                currency = value.ToUpper();
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < 0) throw new InvalidFinancialDataException("Invalid Quantity");
                quantity = value;
            }
        }

        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set
            {
                if (value < 0) throw new InvalidFinancialDataException("Invalid Price");
                purchasePrice = value;
            }
        }

        public decimal MarketPrice
        {
            get { return marketPrice; }
            set
            {
                if (value < 0) throw new InvalidFinancialDataException("Invalid Price");
                marketPrice = value;
            }
        }

        public virtual decimal CalculateCurrentValue()
        {
            return MarketPrice * Quantity;
        }

        public virtual string GetInstrumentSummary()
        {
            return $"{InstrumentID} | {Name} | {Quantity} | {Currency}";
        }
    }

    class Equity : FinancialInstrument, IRiskAssessable, IReportable
    {
        public string GetRiskCategory()
        {
            return "High";
        }

        public string GenerateReportLine()
        {
            return $"{InstrumentID} | Equity | {Name} | {CalculateCurrentValue():C}";
        }
    }

    class Bond : FinancialInstrument, IRiskAssessable, IReportable
    {
        public string GetRiskCategory()
        {
            return "Medium";
        }

        public string GenerateReportLine()
        {
            return $"{InstrumentID} | Bond | {Name} | {CalculateCurrentValue():C}";
        }
    }

    class FixedDeposit : FinancialInstrument, IRiskAssessable, IReportable
    {
        public string GetRiskCategory()
        {
            return "Low";
        }

        public string GenerateReportLine()
        {
            return $"{InstrumentID} | FixedDeposit | {Name} | {CalculateCurrentValue():C}";
        }
    }

    class MutualFund : FinancialInstrument, IRiskAssessable, IReportable
    {
        public string GetRiskCategory()
        {
            return "High";
        }

        public string GenerateReportLine()
        {
            return $"{InstrumentID} | MutualFund | {Name} | {CalculateCurrentValue():C}";
        }
    }

    class Transaction
    {
        public string TransactionId { get; set; }
        public string InstrumentId { get; set; }
        public string Type { get; set; }
        public int Units { get; set; }
        public DateOnly Date { get; set; }
    }

    class Portfolio
    {
        private List<FinancialInstrument> instruments = new List<FinancialInstrument>();
        private Dictionary<string, FinancialInstrument> lookup = new Dictionary<string, FinancialInstrument>();

        public void AddInstrument(FinancialInstrument instrument)
        {
            if (lookup.ContainsKey(instrument.InstrumentID))
                throw new InvalidFinancialDataException("Duplicate Instrument ID");

            instruments.Add(instrument);
            lookup.Add(instrument.InstrumentID, instrument);
        }

        public void RemoveInstrument(string id)
        {
            if (!lookup.ContainsKey(id)) return;

            var inst = lookup[id];
            instruments.Remove(inst);
            lookup.Remove(id);
        }

        public FinancialInstrument GetInstrumentById(string id)
        {
            if (lookup.TryGetValue(id, out var inst))
                return inst;

            return null;
        }

        public decimal GetTotalPortfolioValue()
        {
            return instruments.Sum(x => x.CalculateCurrentValue());
        }

        public List<FinancialInstrument> GetInstrumentsByRisk(string risk)
        {
            return instruments
                .Where(x => x is IRiskAssessable r && r.GetRiskCategory().Equals(risk, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<FinancialInstrument> GetAll()
        {
            return instruments;
        }

        public void ProcessTransaction(Transaction t)
        {
            if (!lookup.ContainsKey(t.InstrumentId))
                throw new InvalidFinancialDataException("Instrument Not Found");

            var inst = lookup[t.InstrumentId];

            if (t.Type.Equals("Buy", StringComparison.OrdinalIgnoreCase))
                inst.Quantity += t.Units;
            else
            {
                if (inst.Quantity < t.Units)
                    throw new InvalidFinancialDataException("Insufficient Units");

                inst.Quantity -= t.Units;
            }
        }
    }

    class ReportGenerator
    {
        public void GenerateConsoleReport(Portfolio portfolio)
        {
            var data = portfolio.GetAll();

            Console.WriteLine("----- PORTFOLIO SUMMARY -----");

            var grouped = data.GroupBy(x => x.GetType().Name);

            foreach (var group in grouped)
            {
                decimal investment = group.Sum(x => x.PurchasePrice * x.Quantity);
                decimal current = group.Sum(x => x.CalculateCurrentValue());

                Console.WriteLine($"Instrument Type: {group.Key}");
                Console.WriteLine($"Total Investment: {investment:C}");
                Console.WriteLine($"Current Value: {current:C}");
                Console.WriteLine($"Profit/Loss: {(current - investment):C}");
                Console.WriteLine();
            }

            Console.WriteLine($"Overall Portfolio Value: {portfolio.GetTotalPortfolioValue():C}");

            var risk = data
                .OfType<IRiskAssessable>()
                .GroupBy(x => x.GetRiskCategory())
                .Select(g => new { Risk = g.Key, Count = g.Count() });

            Console.WriteLine("Risk Distribution:");
            foreach (var r in risk)
                Console.WriteLine($"{r.Risk}: {r.Count}");
        }

        public void GenerateFileReport(Portfolio portfolio)
        {
            string file = $"PortfolioReport_{DateTime.Now:yyyyMMdd}.txt";

            using StreamWriter writer = new StreamWriter(file);

            writer.WriteLine("----- PORTFOLIO REPORT ------");
            writer.WriteLine($"Generated: {DateTime.Now}");
            writer.WriteLine();

            foreach (var inst in portfolio.GetAll())
            {
                if (inst is IReportable r)
                    writer.WriteLine(r.GenerateReportLine());
            }

            writer.WriteLine();
            writer.WriteLine($"Total Portfolio Value: {portfolio.GetTotalPortfolioValue():C}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Portfolio portfolio = new Portfolio();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Equity eq = new Equity
            {
                InstrumentID = "AB001",
                Name = "INFY",
                Currency = "INR",
                Quantity = 100,
                PurchasePrice = 1500,
                MarketPrice = 1650,
                PurchaseDate = new DateOnly(2024, 1, 1)
            };

            Bond bond = new Bond
            {
                InstrumentID = "ML001",
                Name = "GovBond",
                Currency = "INR",
                Quantity = 200,
                PurchasePrice = 1000,
                MarketPrice = 1100,
                PurchaseDate = new DateOnly(2023, 5, 1)
            };

            MutualFund mf = new MutualFund
            {
                InstrumentID = "ZK001",
                Name = "AxisGrowth",
                Currency = "INR",
                Quantity = 50,
                PurchasePrice = 2000,
                MarketPrice = 2300,
                PurchaseDate = new DateOnly(2024, 3, 1)
            };

            FixedDeposit fd = new FixedDeposit
            {
                InstrumentID = "RC001",
                Name = "HDFCFD",
                Currency = "INR",
                Quantity = 10,
                PurchasePrice = 10000,
                MarketPrice = 10500,
                PurchaseDate = new DateOnly(2022, 6, 1)
            };

            portfolio.AddInstrument(eq);
            portfolio.AddInstrument(bond);
            portfolio.AddInstrument(mf);
            portfolio.AddInstrument(fd);

            Transaction[] transactions =
            {
                new Transaction{TransactionId="T1",InstrumentId="AB001",Type="Buy",Units=10,Date=new DateOnly(2025,1,1)},
                new Transaction{TransactionId="T2",InstrumentId="ML001",Type="Sell",Units=20,Date=new DateOnly(2025,2,1)}
            };

            List<Transaction> transactionList = transactions.ToList();

            foreach (var t in transactionList)
                portfolio.ProcessTransaction(t);

            ReportGenerator report = new ReportGenerator();

            report.GenerateConsoleReport(portfolio);
            report.GenerateFileReport(portfolio);
        }
    }
}