using System;
using System.Collections.Generic;

namespace WebApiDbFirst.Models;

public partial class Student
{
    public int RollNo { get; set; }

    public string? StudentName { get; set; }

    public int? Marks { get; set; }

    public string? City { get; set; }
}
