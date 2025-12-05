using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Title { get; set; }

    public string? Degree { get; set; }

    public virtual Employee? Employee { get; set; }
}
