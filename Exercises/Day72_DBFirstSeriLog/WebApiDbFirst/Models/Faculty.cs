using System;
using System.Collections.Generic;

namespace WebApiDbFirst.Models;

public partial class Faculty
{
    public int Facultyid { get; set; }

    public string Facultyname { get; set; } = null!;

    public string? Subjectname { get; set; }
}
