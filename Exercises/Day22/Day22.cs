using System;
using System.Collections.Generic;

class Ride
{
    public int RideID { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal Fare { get; set; }

    public Ride(int rideId, string from, string to, decimal fare)
    {
        RideID = rideId;
        From = from;
        To = to;
        Fare = fare;
    }
}

class OLADriver
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string VehicleNo { get; set; }
    public List<Ride> Rides { get; set; }

    public OLADriver(int id, string name, string vehicleNo)
    {
        Id = id;
        Name = name;
        VehicleNo = vehicleNo;
        Rides = new List<Ride>();
    }

    public decimal GetTotalFare()
    {
        decimal total = 0;
        foreach (var ride in Rides)
        {
            total += ride.Fare;
        }
        return total;
    }
}

internal class Day22
{
    static void Main()
    {
        List<OLADriver> drivers = new List<OLADriver>();

        OLADriver d1 = new OLADriver(1, "Aman", "UP53DR545");
        d1.Rides.Add(new Ride(101, "Gorakhpur", "Lucknow", 550));
        d1.Rides.Add(new Ride(102, "Kanpur", "Delhi", 1200));

        OLADriver d2 = new OLADriver(2, "Ritesh", "DL01DC8901");
        d2.Rides.Add(new Ride(201, "New Delhi", "Noida", 300));
        d2.Rides.Add(new Ride(202, "Noida", "Gurgaon", 450));
        d2.Rides.Add(new Ride(203, "Gurgaon", "Delhi", 400));

        drivers.Add(d1);
        drivers.Add(d2);

        foreach (var driver in drivers)
        {
            Console.WriteLine("\nDriver ID: " + driver.Id);
            Console.WriteLine("Name: " + driver.Name);
            Console.WriteLine("Vehicle No: " + driver.VehicleNo);
            Console.WriteLine("Rides:");

            foreach (var ride in driver.Rides)
            {
                Console.WriteLine(
                    "RideID: " + ride.RideID +
                    ", From: " + ride.From +
                    ", To: " + ride.To +
                    ", Fare: " + ride.Fare
                );
            }

            Console.WriteLine("Total Fare: " + driver.GetTotalFare());
        }
    }
}