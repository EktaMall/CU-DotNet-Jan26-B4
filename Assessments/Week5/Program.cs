namespace Week5_Assessment
{
    internal class Program
    {
        class RestrictedDestinationException : Exception
        {
            public string DeniedLocation { get; set; }
            public RestrictedDestinationException(string location)
                : base($"Shipment to restricted destination: {location}")
            {
                DeniedLocation = location;
            }
        }

        class InsecurePackagingException : Exception
        {
            public InsecurePackagingException(string message) : base(message) { }
        }

        interface ILoggable
        {
            public void SaveLog(string message);
        }

        class LogManager : ILoggable
        {
            public void SaveLog(string message)
            {
                string dir = @"..\..\..\";
                string file = @"shipment_audit.log";
                string path = dir + file;
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"{message}");
                }
            }
        }

        abstract class Shipment
        {
            public string TrackingId { get; set; }

            public double Weight { get; set; }

            public string Destination { get; set; }

            public bool Fragile { get; set; }
            public bool Reinforced { get; set; }
            public abstract void ProcessShipment();
        }
        class ExpressShipment : Shipment
        {
            public override void ProcessShipment()
            {
                if (Weight <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Weight));

                List<string> restrictedZones = new List<string>
                {
                    "North Pole",
                    "Unknown Island"
                };

                foreach (string zone in restrictedZones)
                {
                    if (zone == Destination)
                        throw new RestrictedDestinationException(Destination);
                }

                if (Fragile && !Reinforced)
                    throw new InsecurePackagingException("Fragile shipment requires reinforced packaging.");

                Console.WriteLine($"Express shipment {TrackingId} processed successfully.");
            }
        }

        class HeavyFreight : Shipment
        {
            public override void ProcessShipment()
            {
                if (Weight <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Weight));

                List<string> restrictedZones = new List<string>
                {
                    "North Pole",
                    "Unknown Island"
                };

                foreach (string zone in restrictedZones)
                {
                    if (zone == Destination)
                        throw new RestrictedDestinationException(Destination);
                }

                if (Weight > 1000)
                    Console.WriteLine("Heavy Lift permit granted.");

                Console.WriteLine($"Heavy freight shipment {TrackingId} processed successfully.");
            }
        }
        static void Main(string[] args)
        {
            LogManager logger = new LogManager();

            List<Shipment> shipments = new List<Shipment>
            {
                new ExpressShipment
                {
                        TrackingId = "101",
                        Weight = 10,
                        Destination = "UK",
                        Fragile = true,
                        Reinforced = true
                },

                new ExpressShipment
                {
                    TrackingId = "102",
                    Weight = 55,
                    Destination = "North Pole",
                    Fragile = false,
                    Reinforced = false
                },

                new HeavyFreight
                {
                    TrackingId = "103",
                    Weight= 1200,
                    Destination = "India"
                },

                new HeavyFreight
                {
                    TrackingId = "104",
                    Weight = 0,
                    Destination = "Rome"
                }
            };

            foreach (Shipment shipment in shipments)
            {
                try
                {
                    shipment.ProcessShipment();
                    logger.SaveLog($"SUCCESS: Shipment {shipment.TrackingId} processed");
                }
                catch (RestrictedDestinationException e)
                {
                    logger.SaveLog($"SECURITY ALERT: Shipment {shipment.TrackingId} | {e.Message}");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    logger.SaveLog($"DATA ENTRY ERROR: {e.Message}");
                }
                catch (Exception e)
                {
                    logger.SaveLog($"GENERAL ERROR: {e.Message}");
                }
                finally
                {
                    Console.WriteLine($"Processing attempt finished for ID: {shipment.TrackingId}");
                }
            }
            Console.WriteLine("\nAll shipments processed. Check shipment_audit.log for details.");
        }
    }
}
