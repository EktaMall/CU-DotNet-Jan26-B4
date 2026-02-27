namespace Day24_02
{
    internal class Day24_02
    {
        static void Main(string[] args)
        {
            SortedDictionary<double, string> leaderboard = new SortedDictionary<double, string>();
            leaderboard.Add(55.42, "SwiftRacer");
            leaderboard.Add(52.10, "SpeedDemon");
            leaderboard.Add(58.91, "SteadyEddie");
            leaderboard.Add(51.05, "TurboTom");

            foreach (var item in leaderboard)
            {
                Console.WriteLine($"player name : {item.Value} time : {item.Key:F2}");
            }

            Console.WriteLine();
            var fastest = leaderboard.First();
            Console.WriteLine($"fastest player :{fastest}");

            Console.WriteLine();

            var value = "SteadyEddie";

            var key = leaderboard.FirstOrDefault(x => x.Value == value).Key;
            leaderboard.Remove(key);

            leaderboard.Add(54.00, "SteadyEddie");



            Console.WriteLine($"----after update----");
            Console.WriteLine();
            foreach (var item in leaderboard)
            {
                Console.WriteLine($"player name : {item.Value} time : {item.Key:F2}");
            }
        }
    }
}