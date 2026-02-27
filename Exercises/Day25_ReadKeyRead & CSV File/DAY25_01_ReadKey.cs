using System.Net.NetworkInformation;

namespace Day25
{
    internal class DAY25
    {
        static void Main(string[] args)
        {
            string pin = "";
            Console.Write("Enter 4 digit PIN : ");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.KeyChar >= '0' && key.KeyChar <= '9')
                {
                    if (pin.Length < 4)
                    {
                        pin = pin + key.KeyChar;
                        Console.Write("*");
                    }
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (pin.Length > 0)
                    {
                        pin = pin.Substring(0, pin.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (pin.Length == 4)
                    {
                        break;
                    }
                }
            }
        }
    }
}

