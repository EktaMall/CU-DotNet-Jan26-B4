namespace Week9
{
    class Student
    {
        public int StudentID { get; set; }
        public string SName { get; set; }

        public static Dictionary<Student, int> demo = new Dictionary<Student, int>();

        public override bool Equals(object obj)
        {
            Student stemp = obj as Student;
            return this.StudentID == stemp.StudentID && this.SName == stemp.SName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StudentID, SName);
        }
    }

    internal class Program
    {
        static Dictionary<Student, int> AddStudent(Student student, int marks)
        {
            if (!Student.demo.ContainsKey(student))
            {
                Student.demo.Add(student, marks);
            }
            else
            {
                if (Student.demo[student] < marks)
                {
                    Student.demo[student] = marks;
                }
            }
            return Student.demo;
        }

        static void DisplayRecords()
        {
            foreach (var i in Student.demo)
            {
                Console.WriteLine($"{i.Key.StudentID} {i.Key.SName} {i.Value}");
            }
        }

        static void Main(string[] args)
        {
            Student s1 = new Student { StudentID = 101, SName = "Riya" };
            Student s2 = new Student { StudentID = 102, SName = "Arjun" };
            Student s3 = new Student { StudentID = 103, SName = "Neha" };
            Student s4 = new Student { StudentID = 103, SName = "Neha" };

            AddStudent(s1, 72);
            AddStudent(s2, 88);
            AddStudent(s3, 64);
            AddStudent(s4, 40);

            DisplayRecords();

            AddStudent(s1, 90);
            AddStudent(s2, 88);
            AddStudent(s3, 55);
            AddStudent(s4, 45);

            DisplayRecords();
        }
    }
}