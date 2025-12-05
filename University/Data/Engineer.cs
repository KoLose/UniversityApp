using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Engineer
{
    public int EngineerId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Specialty { get; set; }

    public virtual Employee? Employee { get; set; }
}
