using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositeries
{
    public interface IStudentRepositery
    {
        IEnumerable<Student> GetAll();
        Student? GetById(int id);
        void Add(Student student);
        bool Update(Student student);
        bool Delete(int id);
    }
}
