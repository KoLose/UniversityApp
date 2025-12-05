using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Specialty
{
    public int SpecialtyId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? DeptId { get; set; }

    public virtual Department? Dept { get; set; }

    public virtual ICollection<SpecialtySubject> SpecialtySubjects { get; set; } = new List<SpecialtySubject>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
