using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace CSLearning
{
    internal class gym_membership
    {
        static void Main(string[] args)
        {
            Console.Write("Do you want Treadmill service? (yes/no): ");
            bool useTreadmill = Console.ReadLine().ToLower() == "yes";

            Console.Write("Do you want Weight Lifting service? (yes/no): ");
            bool useWeightLifting = Console.ReadLine().ToLower() == "yes";

            Console.Write("Do you want Zumba classes? (yes/no): ");
            bool useZumba = Console.ReadLine().ToLower() == "yes";

            decimal amount = CalculateMonthlyMembership(
                useTreadmill,
                useWeightLifting,
                useZumba
            );

            if (amount > 0)
            {
                Console.WriteLine($"Monthly Membership Amount: Rs. {amount:F2}");
            }
        }

        static decimal CalculateMonthlyMembership(
            bool useTreadmill,
            bool useWeightLifting,
            bool useZumba)
        {
            if (!useTreadmill && !useWeightLifting && !useZumba)
            {
                Console.WriteLine("At least one service must be selected.");
                return 0;
            }

            decimal totalAmount = 1000;

            if (useTreadmill)
                totalAmount += 300;

            if (useWeightLifting)
                totalAmount += 500;

            if (useZumba)
                totalAmount += 250;

            decimal gst = totalAmount * 0.05m;
            totalAmount += gst;

            return totalAmount;
        }
    }
}


