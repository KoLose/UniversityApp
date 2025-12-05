using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string? Abbreviation { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
