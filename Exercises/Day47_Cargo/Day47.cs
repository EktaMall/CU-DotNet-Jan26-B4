namespace Week09Work
{
    public class Item
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Category { get; set; }
        public Item(string name, double weight, string category)
        {
            Name = name;
            Weight = weight;
            Category = category;
        }

    }
    public class Container
    {
        public string ContainerID { get; set; }
        public List<Item> items = new List<Item>();
        public Container(string ID, List<Item> item)
        {
            ContainerID = ID;
            items = new List<Item>(item);
        }
    }
    internal class Program
    {
        static void FindHeavyContainers(List<List<Container>> container, double threshhold)
        {
            List<string> data = new List<string>();

            var exceed = container.SelectMany(y => y).
                Select(x => new
                {
                    ID = x.ContainerID,
                    totalWeight = x.items.Sum(y => y.Weight)

                }).Where(x => x.totalWeight > threshhold);
            foreach (var i in exceed)
            {
                Console.WriteLine($"{i.ID}");
            }

        }
        static void getItemsByCount(Container c)
        {
            var cont = c.items.GroupBy(y => y.Category).Select(x => new
            {
                x.Key,
                cnt = x.Count()
            }).ToDictionary(x => x.Key, y => y.cnt);

            foreach (var i in cont)
            {
                Console.WriteLine(i.Key + " " + i.Value);
            }
        }
        static void FlattenAndSortShipment(List<List<Container>> container)
        {
            var flatten = container.SelectMany(x => x).SelectMany(x => x.items).GroupBy(x => x.Name).Select(x => x.First())
                .OrderBy(x => x.Category)
                .ThenByDescending(y => y.Weight).ToList();

            foreach (var i in flatten)
            {
                Console.WriteLine($"{i.Category} | {i.Name}");
            }
        }
        static void Main(string[] args)
        {
            var contain = new List<Container> {
            new Container("C001", new List<Item>
            {
                new Item("Lamp",3.0,"Decor"),
                new Item("Vase",12.0,"Decor"),
                new Item("Mirror", 9.0,"Decor")
            }
            ),

            new Container("C002", new List<Item>
            {
            new Item("Mango", 0.2, "Food"),
            new Item("Apple", 0.2, "Food"),
            new Item("DietCoke", 1.0, "Food"),
            new Item("Apple", 0.2, "Food"),
            }
            ),

            new Container("C003", new List<Item>
            {
            new Item("Chair", 15.0, "Furniture"),
            new Item("Table", 7.5, "Furniture")
            }
            ),
        };

            List<List<Container>> demo = new List<List<Container>>();
            demo.Add(contain);
            FindHeavyContainers(demo, 10);
            FlattenAndSortShipment(demo);
            foreach (var i in contain)
            {
                getItemsByCount(i);
            }
        }
    }
}
