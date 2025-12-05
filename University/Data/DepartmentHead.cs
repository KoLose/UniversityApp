using System;
using System.Collections.Generic;

namespace University.Data;

public partial class DepartmentHead
{
    public int DepartmentHeadId { get; set; }

    public int? EmployeeId { get; set; }

    public int? Experience { get; set; }

    public virtual Employee? Employee { get; set; }
}
