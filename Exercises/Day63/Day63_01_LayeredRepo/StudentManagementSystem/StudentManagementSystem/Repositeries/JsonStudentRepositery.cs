using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudentManagementSystem.Models;
using System.Text.Json;

namespace StudentManagementSystem.Repositeries
{
    public class JsonStudentRepositery : IStudentRepositery
    {
        private readonly string _filePath;
        private readonly object _lock = new();
        private List<Student> _students;

        public JsonStudentRepositery(string? filePath = null)
        {
            _filePath = string.IsNullOrWhiteSpace(filePath)
                ? Path.Combine(AppContext.BaseDirectory, "students.json")
                : filePath!;

            if (!File.Exists(_filePath))
            {
                _students = new List<Student>();
                Persist();
            }
            else
            {
                _students = LoadFromFile();
            }
        }

        public IEnumerable<Student> GetAll()
        {
            lock (_lock)
            {
                return _students.Select(s => new Student { Id = s.Id, Name = s.Name, Grade = s.Grade }).ToList();
            }
        }

        public Student? GetById(int id)
        {
            lock (_lock)
            {
                var s = _students.FirstOrDefault(x => x.Id == id);
                return s is null ? null : new Student { Id = s.Id, Name = s.Name, Grade = s.Grade };
            }
        }

        public void Add(Student student)
        {
            lock (_lock)
            {
                var nextId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
                var toAdd = new Student { Id = nextId, Name = student.Name, Grade = student.Grade };
                _students.Add(toAdd);
                Persist();
            }
        }

        public bool Update(Student student)
        {
            lock (_lock)
            {
                var existing = _students.FirstOrDefault(s => s.Id == student.Id);
                if (existing == null) return false;
                existing.Name = student.Name;
                existing.Grade = student.Grade;
                Persist();
                return true;
            }
        }

        public bool Delete(int id)
        {
            lock (_lock)
            {
                var existing = _students.FirstOrDefault(s => s.Id == id);
                if (existing == null) return false;
                _students.Remove(existing);
                Persist();
                return true;
            }
        }

        private List<Student> LoadFromFile()
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<Student>>(json, opts) ?? new List<Student>();
            }
            catch
            {
                return new List<Student>();
            }
        }

        private void Persist()
        {
            var opts = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_students, opts);
            File.WriteAllText(_filePath, json);
        }
    }
}