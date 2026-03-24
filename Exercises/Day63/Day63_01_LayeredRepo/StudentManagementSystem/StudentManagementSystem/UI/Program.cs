using StudentManagementSystem.Models;
using StudentManagementSystem.Repositeries;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select Storage: 1. List  2. JSON");
            int choice = int.Parse(Console.ReadLine());

            IStudentRepositery repo = choice == 1
                ? new ListStudentRepositery()
                : new JsonStudentRepositery();

            StudentService service = new StudentService(repo);

            while (true)
            {
                Console.WriteLine("\n1.Add 2.View 3.Update 4.Delete 5.Exit");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Grade: ");
                        int grade = int.Parse(Console.ReadLine());

                        service.AddStudent(name, grade);
                        break;

                    case 2:
                        var students = service.GetAll();
                        foreach (var s in students)
                            Console.WriteLine(s);
                        break;

                    case 3:
                        Console.Write("Enter Id: ");
                        int uid = int.Parse(Console.ReadLine());

                        Console.Write("New Name: ");
                        string newName = Console.ReadLine();

                        Console.Write("New Grade: ");
                        int newGrade = int.Parse(Console.ReadLine());

                        bool updated = service.Update(uid, newName, newGrade); 
                        Console.WriteLine(updated ? "Updated" : "Not Found");
                        break;

                    case 4:
                        Console.Write("Enter Id: ");
                        int did = int.Parse(Console.ReadLine());

                        bool deleted = service.Delete(did);

                        Console.WriteLine(deleted ? "Deleted" : "Not Found");
                        break;

                    case 5:
                        return;
                }
            }
        }
    }
}