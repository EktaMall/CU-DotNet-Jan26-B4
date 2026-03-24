using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    internal interface IStudentService
    {
        Student AddStudent(string name, int grade);
        IEnumerable<Student> GetAll();
        Student? GetById(int id);
        bool Update(int id, string name, int grade);
        bool Delete(int id);
    }
}
