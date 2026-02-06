using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDailyLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dir = @"..\..\..\";

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Directory doesn't exist");
                return;
            }
            string file = "journal.txt";
            string path = dir + file;

            Console.WriteLine("DAILY REFLECTION");
            string data = Console.ReadLine();
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(data);
            }
        }
    }
}
