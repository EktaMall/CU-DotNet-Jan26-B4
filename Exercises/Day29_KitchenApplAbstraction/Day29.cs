namespace ConsoleApp1
{
    interface ITimer
    {
        void SetTimer(int minutes);
    }
    interface ISmart
    {
        void ConnectWifi(string wifiName);
    }

    abstract class KitchenElecApps
    {
        public double Voltage { get; set; }
        public string ModelName { get; set; }
        public double Price { get; set; }
        public abstract void cook();
    }

    class Microwave : KitchenElecApps, ITimer, ISmart
    {
        public override void cook()
        {
            Console.WriteLine("cooking in microwave");
        }

        public void SetTimer(int minutes)
        {
            Console.WriteLine($"timer : {minutes} minutes ");
        }

        public void ConnectWifi(string wifiname)
        {
            Console.WriteLine($"wifi from {wifiname} is connected to microwave");
        }
    }
    class kettle : KitchenElecApps
    {
        public override void cook()
        {
            Console.WriteLine("cooking in kettle");
        }
    }
    class oven : KitchenElecApps
    {
        public override void cook()
        {
            Console.WriteLine("cooking in oven");
        }
        public void preheat()
        {
            Console.WriteLine("preheat oven");
        }
    }
    class AirFryer : KitchenElecApps, ITimer
    {
        public override void cook()
        {
            Console.WriteLine("cooking in AirFryer");
        }

        public void SetTimer(int minutes)
        {
            Console.WriteLine($"timer : {minutes} minutes");
        }
    }

    internal class Day29
    {
        static void Main(string[] args)
        {
            List<KitchenElecApps> list1 = new List<KitchenElecApps>()
            {
                new Microwave
                {
                    ModelName = "micro",
                    Price  = 50000,
                    Voltage = 30
                },
                new kettle
                {
                    ModelName = "kettle",
                    Price = 2000,
                    Voltage = 10
                },
                new oven
                {
                    ModelName = "oven",
                    Price = 45000,
                    Voltage = 35
                },
                new AirFryer
                {
                    ModelName = "air",
                    Price = 5000,
                    Voltage = 25
                }
            };

            Console.WriteLine("\b                          KITCHEN APPLIANCES DETAILS");
            foreach (KitchenElecApps app in list1)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine($"model name : {app.ModelName,-10} | " +
                    $"price: {app.Price,-10} | " +
                    $"voltage required : {app.Voltage + "V",-10}");



                if (app is Microwave)
                {
                    var m = (Microwave)app;
                    m.cook();
                    m.SetTimer(5);
                    m.ConnectWifi("ks");
                }

                if (app is kettle)
                {
                    var k = (kettle)app;
                    k.cook();

                }

                if (app is oven)
                {
                    var o = (oven)app;
                    o.cook();
                    o.preheat();
                }

                if (app is AirFryer)
                {
                    var a = (AirFryer)app;
                    a.cook();
                    a.SetTimer(25);
                }

            }

        }
    }
}