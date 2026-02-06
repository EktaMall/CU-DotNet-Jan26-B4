using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Week4
{
    class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime DepartureTime { get; set; }

        public int CompareTo(Flight? other)
        {
            if (other == null) return 1;
            return Price.CompareTo(other.Price);
        }

        public override string ToString()
        {
            return $"Flight {FlightNumber} + Price: {Price} + Duration: {Duration} + Departure: {DepartureTime}";
        }
    }

    class DurationComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.Duration.CompareTo(y.Duration);
        }
    }

    class DepartureComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.DepartureTime.CompareTo(y.DepartureTime);
        }
    }


    internal class Class2
    {
        static void Main(string[] args)
        {
            List<Flight> flights = new List<Flight>
                {
                    new Flight 
                    {
                        FlightNumber = "101",
                        Price = 7000,
                        Duration = new TimeSpan(2, 30 ,0),
                        DepartureTime = new DateTime(2026, 1, 29, 6, 30, 0),
                    },

                    new Flight
                    {
                        FlightNumber = "103",
                        Price = 5000,
                        Duration = new TimeSpan(3, 15 ,0),
                        DepartureTime = new DateTime(2026, 1, 30, 8, 45, 0),
                    },

                    null,

                    new Flight
                    {
                        FlightNumber = "102",
                        Price = 10000,
                        Duration = new TimeSpan(1, 45 ,0),
                        DepartureTime = new DateTime(2026, 1, 29, 10, 30, 0),
                    },
            };
            Console.WriteLine( "Economy View");
            flights.Sort();
            foreach (var flight in flights)
            {
                if (flight != null)
                    Console.WriteLine(flight);
            }
            Console.WriteLine();

            Console.WriteLine("Business Runner View");
            flights.Sort(new DurationComparer());
            foreach (var flight in flights)
            {
                if (flight != null)
                    Console.WriteLine(flight);
            }
            Console.WriteLine();

            Console.WriteLine( "Early Bird view");
            flights.Sort(new DepartureComparer());
            foreach (var flight in flights)
            {
                if (flight != null)
                    Console.WriteLine(flight);
            }
        }
    }
}
