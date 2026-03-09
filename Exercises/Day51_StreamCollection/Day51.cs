using System.Threading.Channels;

namespace Demo2
{
    class CreatorStats
    {
        public string CreatorName { get; set; }
        public double[] WeeklyLikes { get; set; }
    }

    internal class Program
    {
        public List<CreatorStats> EngagementBoard = new List<CreatorStats>();

        public void RegisterCreator(CreatorStats record)
        {
            EngagementBoard.Add(record);
        }

        public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double Threshold)
        {
            var data = records.Select(x => new
            {
                Name = x.CreatorName,
                cnt = x.WeeklyLikes.Count(y => y >= Threshold)
            }).ToDictionary(x => x.Name, y => y.cnt);

            return data;
        }

        public double CalculateAverageLikes()
        {
            var data = EngagementBoard.Select(x => x.WeeklyLikes.Average(y => y));
            return data.Average();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            int choice = 0;

            while (choice != 4)
            {
                Console.WriteLine("Enter Choice to Proceed");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Content Creator Name:");
                        string name = Console.ReadLine();

                        double[] week = new double[4];
                        Console.WriteLine("Enter Weekly Engagement Scores");

                        for (int i = 0; i < 4; i++)
                        {
                            double.TryParse(Console.ReadLine(), out double result);
                            week[i] = result;
                        }

                        List<CreatorStats> list = new List<CreatorStats>();
                        list.Add(new CreatorStats { CreatorName = name, WeeklyLikes = week });

                        for (int i = 0; i < list.Count; i++)
                        {
                            p.RegisterCreator(list[i]);
                        }
                        break;

                    case 2:
                        Console.WriteLine("Enter the engagement threshold value");
                        double.TryParse(Console.ReadLine(), out double r);
                        double threshhold = r;

                        var x = p.GetTopPostCounts(p.EngagementBoard, threshhold);

                        foreach (var i in x)
                        {
                            Console.WriteLine(i.Key + " - " + i.Value);
                        }
                        break;

                    case 3:
                        Console.WriteLine(p.CalculateAverageLikes());
                        break;

                    case 4:
                        return;
                }
            }
        }
    }
}