namespace ExpenseSplliter
{
    internal class Program
    {
        public static List<string> ExpenseSplitter(Dictionary<string, double> dict)
        {
            List<string> result = new List<string>();

            Queue<KeyValuePair<string, double>> payer = new();
            Queue<KeyValuePair<string, double>> receiver = new();

            double totalSum = dict.Values.Sum();
            int persons = dict.Count;
            double share = totalSum / persons;

            foreach (var person in dict)
            {
                double diff = person.Value - share;

                if (diff > 0)
                    payer.Enqueue(new KeyValuePair<string, double>(person.Key, diff));
                else if (diff < 0)
                    receiver.Enqueue(new KeyValuePair<string, double>(person.Key, -diff));
            }

            while (payer.Count > 0 && receiver.Count > 0)
            {
                var p = payer.Dequeue();
                var r = receiver.Dequeue();

                double amount = Math.Min(p.Value, r.Value);

                result.Add($"{r.Key} gives {amount:F2} to {p.Key}");

                if (p.Value > amount)
                    payer.Enqueue(new KeyValuePair<string, double>(p.Key, p.Value - amount));

                if (r.Value > amount)
                    receiver.Enqueue(new KeyValuePair<string, double>(r.Key, r.Value - amount));
            }

            return result;
        }

        static void Main(string[] args)
        {
            Dictionary<string, double> dict = new Dictionary<string, double>()
            {
                {"Ekta", 2000 },
                {"Rishi", 1500 },
                {"Aniketh", 900 }
            };

            var settlements = ExpenseSplitter(dict);

            foreach (var s in settlements)
                Console.WriteLine(s);
        }
    }
}