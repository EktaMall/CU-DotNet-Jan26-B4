namespace Week2_Assessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] policyHolderNames = new string[5];
            decimal[] annualPremiums = new decimal[5];

            for (int i = 0; i < 5; i++)
            {
                while (true)
                {
                    Console.Write($"Enter name of policy holder {i + 1}: ");
                    string nameInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(nameInput))
                    {
                        policyHolderNames[i] = nameInput;
                        break;
                    }

                    Console.WriteLine("Name cannot be empty. Please re-enter.");
                }

                while (true)
                {
                    Console.Write($"Enter annual premium for {policyHolderNames[i]}: ");
                    bool isValid = decimal.TryParse(Console.ReadLine(), out decimal premium);

                    if (isValid && premium > 0)
                    {
                        annualPremiums[i] = premium;
                        break;
                    }

                    Console.WriteLine("Premium must be a number greater than 0. Please re-enter.");
                }

                Console.WriteLine();
            }

            decimal totalPremium = 0;
            decimal highestPremium = annualPremiums[0];
            decimal lowestPremium = annualPremiums[0];

            for (int i = 0; i < 5; i++)
            {
                totalPremium += annualPremiums[i];

                if (annualPremiums[i] > highestPremium)
                    highestPremium = annualPremiums[i];

                if (annualPremiums[i] < lowestPremium)
                    lowestPremium = annualPremiums[i];
            }

            decimal averagePremium = totalPremium / 5;
            Console.WriteLine();
            Console.WriteLine("Insurance Premium Summary");
            Console.WriteLine("--------------------------");
            Console.WriteLine($"{"Name",-15} {"Premium",-15} {"Category"}");
            Console.WriteLine("-----------------------------------------");

            for (int i = 0; i < 5; i++)
            {
                string category;

                if (annualPremiums[i] < 10000)
                    category = "LOW";
                else if (annualPremiums[i] <= 25000)
                    category = "MEDIUM";
                else
                    category = "HIGH";

                Console.WriteLine(
                    $"{policyHolderNames[i].ToUpper(),-15} " +
                    $"{annualPremiums[i],-15:F2} " +
                    $"{category}"
                );
            }

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Total Premium    : {totalPremium:F2}");
            Console.WriteLine($"Average Premium  : {averagePremium:F2}");
            Console.WriteLine($"Highest Premium  : {highestPremium:F2}");
            Console.WriteLine($"Lowest Premium   : {lowestPremium:F2}");
        }
    }
}