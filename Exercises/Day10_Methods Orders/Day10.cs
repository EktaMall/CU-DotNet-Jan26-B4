namespace Day10
{
    internal class Program
    {

        static void Main()
        {
            const int DAYS = 7;

            decimal[] weeklySales = new decimal[DAYS];
            string[] salesCategories = new string[DAYS];

            ReadWeeklySales(weeklySales);

            decimal totalSales = CalculateTotal(weeklySales);
            decimal averageSales = CalculateAverage(totalSales, DAYS);

            decimal highestSale = FindHighestSale(weeklySales, out int highestDay);
            decimal lowestSale = FindLowestSale(weeklySales, out int lowestDay);

            bool isFestivalWeek = true;
            decimal discount = CalculateDiscount(totalSales, isFestivalWeek);

            decimal taxableAmount = totalSales - discount;
            decimal tax = CalculateTax(taxableAmount);

            decimal finalPayable = CalculateFinalAmount(totalSales, discount, tax);

            GenerateSalesCategory(weeklySales, salesCategories);

            Console.WriteLine("\nWeekly Sales Summary");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Total Sales        : {totalSales:N2}");
            Console.WriteLine($"Average Daily Sale : {averageSales:N2}\n");

            Console.WriteLine($"Highest Sale       : {highestSale:N2} (Day {highestDay})");
            Console.WriteLine($"Lowest Sale        : {lowestSale:N2}  (Day {lowestDay})\n");

            Console.WriteLine($"Discount Applied   : {discount:N2}");
            Console.WriteLine($"Tax Amount         : {tax:N2}");
            Console.WriteLine($"Final Payable      : {finalPayable:N2}\n");

            Console.WriteLine("Day-wise Category:");
            for (int i = 0; i < DAYS; i++)
            {
                Console.WriteLine($"Day {i + 1} : {salesCategories[i]}");
            }
        }


        static void ReadWeeklySales(decimal[] sales)
        {
            for (int i = 0; i < sales.Length; i++)
            {
                while (true)
                {
                    Console.Write($"Enter sales for Day {i + 1}: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0)
                    {
                        sales[i] = value;
                        break;
                    }
                    Console.WriteLine("Invalid input. Enter a non-negative value.");
                }
            }
        }

        static decimal CalculateTotal(decimal[] sales)
        {
            decimal total = 0;
            for (int i = 0; i < sales.Length; i++)
            {
                total += sales[i];
            }
            return total;
        }

        static decimal CalculateAverage(decimal total, int days)
        {
            return total / days;
        }

        static decimal FindHighestSale(decimal[] sales, out int day)
        {
            decimal max = sales[0];
            day = 1;

            for (int i = 1; i < sales.Length; i++)
            {
                if (sales[i] > max)
                {
                    max = sales[i];
                    day = i + 1;
                }
            }
            return max;
        }

        static decimal FindLowestSale(decimal[] sales, out int day)
        {
            decimal min = sales[0];
            day = 1;

            for (int i = 1; i < sales.Length; i++)
            {
                if (sales[i] < min)
                {
                    min = sales[i];
                    day = i + 1;
                }
            }
            return min;
        }

        static decimal CalculateDiscount(decimal total)
        {
            return total >= 50000 ? total * 0.10m : total * 0.05m;
        }

        static decimal CalculateDiscount(decimal total, bool isFestivalWeek)
        {
            decimal discount = CalculateDiscount(total);

            if (isFestivalWeek)
            {
                discount += total * 0.05m;
            }
            return discount;
        }

        static decimal CalculateTax(decimal amount)
        {
            return amount * 0.18m;
        }

        static decimal CalculateFinalAmount(decimal total, decimal discount, decimal tax)
        {
            return total - discount + tax;
        }

        static void GenerateSalesCategory(decimal[] sales, string[] categories)
        {
            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i] < 5000)
                    categories[i] = "Low";
                else if (sales[i] <= 15000)
                    categories[i] = "Medium";
                else
                    categories[i] = "High";
            }
        }

    }
}