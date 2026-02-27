using System;
using System.Collections.Generic;

class Policy
{
    public string HolderName { get; set; }
    public decimal Premium { get; set; }
    public int RiskScore { get; set; }
    public DateTime RenewalDate { get; set; }

    public Policy(string holderName, decimal premium, int riskScore, DateTime renewalDate)
    {
        HolderName = holderName;
        Premium = premium;
        RiskScore = riskScore;
        RenewalDate = renewalDate;
    }
}

class Program
{
    static void Main()
    {
        Dictionary<string, Policy> policies = new Dictionary<string, Policy>();

        policies.Add("P101", new Policy("Rahul", 10000m, 80, DateTime.Now.AddYears(-1)));
        policies.Add("P102", new Policy("Amit", 12000m, 60, DateTime.Now.AddYears(-4)));
        policies.Add("P103", new Policy("Sneha", 15000m, 90, DateTime.Now));

        Console.WriteLine("---- BEFORE BULK ADJUSTMENT ----");
        Display(policies);

        foreach (var item in policies)
        {
            if (item.Value.RiskScore > 75)
            {
                item.Value.Premium = item.Value.Premium + (item.Value.Premium * 0.05m);
            }
        }

        Console.WriteLine("\n---- AFTER 5% PREMIUM INCREASE ----");
        Display(policies);

        List<string> removeKeys = new List<string>();

        foreach (var item in policies)
        {
            if (item.Value.RenewalDate < DateTime.Now.AddYears(-3))
            {
                removeKeys.Add(item.Key);
            }
        }

        foreach (var key in removeKeys)
        {
            policies.Remove(key);
        }

        Console.WriteLine("\n---- AFTER CLEANUP ----");
        Display(policies);

        Console.WriteLine("\n---- SAFE LOOKUP ----");
        Console.WriteLine(SafeLookup(policies, "P103"));
        Console.WriteLine(SafeLookup(policies, "P999"));
    }

    static void Display(Dictionary<string, Policy> policies)
    {
        foreach (var item in policies)
        {
            Console.WriteLine(
                item.Key + " | " +
                item.Value.HolderName + " | " +
                item.Value.Premium + " | " +
                item.Value.RiskScore
            );
        }
    }

    static string SafeLookup(Dictionary<string, Policy> policies, string id)
    {
        if (policies.TryGetValue(id, out Policy policy))
        {
            return "Found → " +
                   policy.HolderName + ", " +
                   policy.Premium + ", " +
                   policy.RiskScore;
        }

        return "Policy Not Found";
    }
}