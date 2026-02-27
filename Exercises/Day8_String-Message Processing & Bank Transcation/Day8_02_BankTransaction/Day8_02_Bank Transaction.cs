using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLearning
{
    internal class Day8__02_
    {
        static void Main(string[] args)
        {
            string data = Console.ReadLine();

            string[] items = data.Split('#');
            string id = items[0];
            string name = items[1];
            string text = items[2];

            string result = text.Trim();

            while (result.Contains("  "))
            {
                result = result.Replace("  ", " ");
            }

            result = result.ToLower();

            bool d = result.Contains("deposit");
            bool w = result.Contains("withdrawal");
            bool t = result.Contains("transfer");

            bool flag = d || w || t;

            string standard = "cash deposit successful";
            string output;

            if (!flag)
            {
                output = "NON-FINANCIAL TRANSACTION";
            }
            else if (result.Equals(standard))
            {
                output = "STANDARD TRANSACTION";
            }
            else
            {
                output = "CUSTOM TRANSACTION";
            }

            Console.WriteLine($"Transaction ID : {id}");
            Console.WriteLine($"Account Holder : {name}");
            Console.WriteLine($"Narration      : {result}");
            Console.WriteLine($"Category       : {output}");
        }
    }
}
