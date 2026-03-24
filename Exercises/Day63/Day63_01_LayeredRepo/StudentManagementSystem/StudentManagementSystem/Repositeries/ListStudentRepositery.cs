using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositeries
{
    public class ListStudentRepositery : IStudentRepositery
    {
        private readonly List<Student> _students = new();

        public IEnumerable<Student> GetAll()
        {
            return _students.Select(s => new Student { Id = s.Id, Name = s.Name, Grade = s.Grade }).ToList();
        }

        public Student? GetById(int id)
        {
            var s = _students.FirstOrDefault(x => x.Id == id);
            return s is null ? null : new Student { Id = s.Id, Name = s.Name, Grade = s.Grade };
        }

        public void Add(Student student)
        {
            var nextId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            var toAdd = new Student { Id = nextId, Name = student.Name, Grade = student.Grade };
            _students.Add(toAdd);
        }

        public bool Update(Student student)
        {
            var existing = _students.FirstOrDefault(s => s.Id == student.Id);
            if (existing == null) return false;
            existing.Name = student.Name;
            existing.Grade = student.Grade;
            return true;
        }

        public bool Delete(int id)
        {
            var existing = _students.FirstOrDefault(s => s.Id == id);
            if (existing == null) return false;
            _students.Remove(existing);
            return true;
        }
    }
}
