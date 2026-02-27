using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3
{

    class ApplicationConfig
    {

        public static string ApplicationName { get; set; }
        static string Environment { get; set; }

        public static int AccessCount { get; set; }

        public static bool IsInitialised { get; set; }

        static ApplicationConfig()
        {

            ApplicationName = "MyApp";
            Environment = "Development";
            AccessCount = 0;
            IsInitialised = true;
            Console.WriteLine("Static constructor executed");


        }
        public static void Initialize(string appName, string env)
        {
            ApplicationName = appName;
            Environment = env;
            IsInitialised = true;
            AccessCount++;
        }

        public static string GetConfigurationSummary()
        {
            AccessCount++;
            return $"app name : {ApplicationName}\n" +
                $"environment : {Environment}\n" +
                $"access count : {AccessCount}\n" +
                $"initialisation status :  {IsInitialised}\n";
        }
        public static void ResetConfiguration()
        {
            ApplicationName = "MyApp";
            Environment = "Development";
            IsInitialised = false;
            AccessCount++;
        }


    }
    internal class Day16
    {
        static void Main(string[] args)
        {
            ApplicationConfig.ApplicationName = "MyApp";
            ApplicationConfig.Initialize("HelloApp", "env");
            Console.WriteLine(ApplicationConfig.GetConfigurationSummary());

            ApplicationConfig.ResetConfiguration();
            Console.WriteLine("----------------reset to default--------------------");
            Console.WriteLine(ApplicationConfig.GetConfigurationSummary());



        }
    }
}
