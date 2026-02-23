using System.Collections;

namespace Day24_01
{
    internal class Day24_01
    {
        static void Main(string[] args)
        {
            Hashtable employeeTable = new Hashtable();

            employeeTable.Add(101, "Alice");
            employeeTable.Add(102, "Bob");
            employeeTable.Add(103, "Charlie");
            employeeTable.Add(104, "Diana");


            if (!employeeTable.ContainsKey(105))
            {
                employeeTable.Add(105, "Edward");
            }
            else
            {
                Console.WriteLine("ID already exists");
            }

            string name = employeeTable[102] as string;
            Console.WriteLine($"name of employee with id 102:  {name}");

            foreach (DictionaryEntry d in employeeTable)
            {
                Console.WriteLine($"ID: {d.Key}, Name: {d.Value}");
            }

            employeeTable.Remove(103);
            int count = employeeTable.Count;
            Console.WriteLine($"count : {count}");





        }
    }
}