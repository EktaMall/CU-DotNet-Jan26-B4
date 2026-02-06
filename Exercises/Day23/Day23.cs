using System;
using System.Text.RegularExpressions;

namespace Day_23_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter first number: ");
                int a = int.Parse(Console.ReadLine());

                Console.Write("Enter second number: ");
                int b = int.Parse(Console.ReadLine());

                int result = a / b;
                Console.WriteLine("Result = " + result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Cannot divide by zero");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number format");
            }
            finally
            {
                Console.WriteLine("Task Completed\n");
            }

            try
            {
                int[] arr = { 15, 30, 45 };
                Console.Write("Enter array index: ");
                int index = int.Parse(Console.ReadLine());

                Console.WriteLine("Value = " + arr[index]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index out of range");
            }
            finally
            {
                Console.WriteLine("Task Completed\n");
            }

            StudentEnrollment();
        }

        static void StudentEnrollment()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Student Name: ");
                    string name = Console.ReadLine();

                    if (!Regex.IsMatch(name, "^[A-Z][a-z]{2,}$"))
                        throw new InvalidStudentNameException("Enter name correctly");

                    Console.Write("Enter Student Age: ");
                    string ageInput = Console.ReadLine();

                    if (!Regex.IsMatch(ageInput, "^[0-9]+$"))
                        throw new FormatException("Enter age correctly");

                    int age = Convert.ToInt32(ageInput);

                    if (age < 18 || age > 60)
                        throw new InvalidStudentAgeException("Age must be between 18 and 60");

                    Console.WriteLine("\nStudent Enrolled Successfully");
                    break;
                }
                catch (Exception ex)
                {
                    Exception wrapped =
                        new Exception("Student Enrollment Failed", ex);

                    Console.WriteLine("\nException Occurred");
                    Console.WriteLine("Message: " + wrapped.Message);
                    Console.WriteLine("Inner Exception: " + wrapped.InnerException.Message);
                    Console.WriteLine("StackTrace: " + wrapped.StackTrace);
                    Console.WriteLine("\nTry Again...\n");
                }
                finally
                {
                    Console.WriteLine("Task Completed\n");
                }
            }
        }
    }

    class InvalidStudentAgeException : Exception
    {
        public InvalidStudentAgeException(string msg) : base(msg) { }
    }

    class InvalidStudentNameException : Exception
    {
        public InvalidStudentNameException(string msg) : base(msg) { }
    }
}
