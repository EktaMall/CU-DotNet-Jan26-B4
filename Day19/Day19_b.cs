namespace Day19_b
{
    abstract class UtilityBill
    {
        public int ConsumerId { get; set; }
        public string ConsumerName { get; set; }
        public decimal UnitsConsumed { get; set; }
        public decimal RatePerUnit { get; set; }

        public abstract decimal CalculateBillAmount();

        public virtual decimal CalculateTax(decimal billAmount)
        {
            return billAmount * 0.05m;
        }

        public void PrintBill()
        {
            decimal billAmount = CalculateBillAmount();
            decimal tax = CalculateTax(billAmount);
            decimal finalAmount = billAmount + tax;

            Console.WriteLine("ID   : " + ConsumerId);
            Console.WriteLine("Name : " + ConsumerName);
            Console.WriteLine("Units: " + UnitsConsumed);
            Console.WriteLine("Pay  : " + finalAmount);
        }
    }

    class ElectricityBill : UtilityBill
    {
        public ElectricityBill(decimal rate)
        {
            RatePerUnit = rate;
        }

        public override decimal CalculateBillAmount()
        {
            decimal amount = UnitsConsumed * RatePerUnit;

            if (UnitsConsumed > 300)
            {
                amount = amount + (amount * 0.10m);
            }

            return amount;
        }
    }

    class WaterBill : UtilityBill
    {
        public override decimal CalculateBillAmount()
        {
            return UnitsConsumed * RatePerUnit;
        }

        public override decimal CalculateTax(decimal billAmount)
        {
            return billAmount * 0.02m;
        }
    }

    class GasBill : UtilityBill
    {
        public override decimal CalculateBillAmount()
        {
            return (UnitsConsumed * RatePerUnit) + 150;
        }

        public override decimal CalculateTax(decimal billAmount)
        {
            return 0;
        }
    }


    class Program
    {
        static void Main()
        {
            List<UtilityBill> bills = new List<UtilityBill>();

            ElectricityBill eb = new ElectricityBill(8);
            eb.ConsumerId = 1;
            eb.ConsumerName = "Ekta";
            eb.UnitsConsumed = 350;

            WaterBill wb = new WaterBill();
            wb.ConsumerId = 2;
            wb.ConsumerName = "Manvi";
            wb.UnitsConsumed = 200;
            wb.RatePerUnit = 5;

            GasBill gb = new GasBill();
            gb.ConsumerId = 3;
            gb.ConsumerName = "Riya";
            gb.UnitsConsumed = 50;
            gb.RatePerUnit = 10;

            bills.Add(eb);
            bills.Add(wb);
            bills.Add(gb);

            foreach (UtilityBill b in bills)
            {
                b.PrintBill();
                Console.WriteLine();
            }
        }
    }
}
