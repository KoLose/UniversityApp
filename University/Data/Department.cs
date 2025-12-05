using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? FacultyId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
