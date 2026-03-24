using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositeries;

namespace StudentManagementSystem.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IStudentRepositery repository;

        public StudentService(IStudentRepositery repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private int NextId()
        {
            var all = repository.GetAll();
            return all.Any() ? all.Max(s => s.Id) + 1 : 1;
        }

        public Student AddStudent(string name, int grade)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
            if (grade < 0 || grade > 100) throw new ArgumentException("Grade must be between 0 and 100.", nameof(grade));

            var student = new Student
            {
                Id = NextId(),
                Name = name.Trim(),
                Grade = grade
            };

            repository.Add(student);
            return student;
        }

        public IEnumerable<Student> GetAll() => repository.GetAll();

        public Student? GetById(int id) => repository.GetById(id);

        public bool Update(int id, string name, int grade)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
            if (grade < 0 || grade > 100) throw new ArgumentException("Grade must be between 0 and 100.", nameof(grade));

            var existing = repository.GetById(id);
            if (existing == null) return false;

            existing.Name = name.Trim();
            existing.Grade = grade;
            return repository.Update(existing);
        }

        public bool Delete(int id) => repository.Delete(id);
    }
}