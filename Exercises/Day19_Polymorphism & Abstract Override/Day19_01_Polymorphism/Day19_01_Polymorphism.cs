namespace Day19_a
{
    abstract class Vehicle
    {
        public string ModelName { get; set; }
        public abstract void Move();
        public virtual string GetFuelStatus()
        {
            return "Fuel level stable";
        }
    }
    class ElectricCar : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine(ModelName + " is gliding silently on battery power.");
        }
        public override string GetFuelStatus()
        {
            return "Battery is at 80%";
        }
    }

    class HeavyTruck : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine(ModelName + " is hauling cargo with high torque diesel power.");
        }
    }

    class CargoPlane : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine(ModelName + " is ascending to 30,000 feet.");
        }
        public override string GetFuelStatus()
        {
            return base.GetFuelStatus() + " | Checking jet fuel reserves...";
        }
    }

    class Program
    {
        static void Main()
        {
            Vehicle[] fleet =
            {
            new ElectricCar { ModelName = "Car" },
            new HeavyTruck { ModelName = "Truck" },
            new CargoPlane { ModelName = "Plane" }
        };

            foreach (Vehicle v in fleet)
            {
                v.Move();
                Console.WriteLine(v.GetFuelStatus());
            }
        }
    }
}

