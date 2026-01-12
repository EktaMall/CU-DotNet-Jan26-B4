using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreetingLibrary;

namespace GreetingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            string message = GreetingHelper.GetGreeting(name);

            Console.WriteLine(message);

            Console.ReadLine();
        }
    }
}
