using System.Linq.Expressions;
using System.Threading.Channels;

namespace PerformanceTracker
{
    class Player
    {
        public string Name { get; set; }
        public int RunScored { get; set; }
        public int BallsFaced { get; set; }
        public bool Isout { get; set; }
        public double StrikeRate { get; set; }
        public double Average { get; set; }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter csv File Path : ");
            string path = Console.ReadLine();

            List<Player> player = new List<Player>();

            try
            {
                using StreamReader sw = new StreamReader(path);
                string read;
                while ((read = sw.ReadLine()) != null)
                {

                    try
                    {
                        string[] input = read.Split(',');
                        string name = input[0];
                        int runs = int.Parse(input[1]);
                        int balls = int.Parse(input[2]);
                        bool Isout = bool.Parse(input[3]);
                        if (balls < 10) continue;
                        double strikeRate = 0;
                        if (balls != 0)
                        {
                            strikeRate = (double)runs / balls * 100;
                        }
                        double average = 0;

                        average = runs;


                        Player p = new Player()
                        {
                            Name = name,
                            BallsFaced = balls,
                            RunScored = runs,
                            Isout = Isout,
                            StrikeRate = strikeRate,
                            Average = average
                        };
                        player.Add(p);
                    }
                    catch (DivideByZeroException e)
                    {

                        Console.WriteLine($"Exception Caught: {e.Message}");
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Exception Caught: {ex.Message}");
                    }
                }

                player = player.OrderBy(x => x.StrikeRate).ToList();

                Console.WriteLine("Details");
                Console.WriteLine($"{"Name",-15}{"RunsScored",-10}{"StrikeRate",-12}{"Average"}");
                foreach (Player i in player)
                {

                    Console.WriteLine($"{i.Name,-15}{i.RunScored,-12}{i.StrikeRate,-12:F2}{i.Average:F2}");
                }
            }
            catch (FileNotFoundException e)
            {

                Console.WriteLine($"Exception Caught: {e.Message}");
            }

        }
    }
}