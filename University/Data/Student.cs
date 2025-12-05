using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Student
{
    public int StudentId { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? FullName { get; set; }

    public int? SpecialtyId { get; set; }

    public string? Login { get; set; }

    public string? PasswordHash { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual Specialty? Specialty { get; set; }
}
