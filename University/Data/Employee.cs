using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? DeptId { get; set; }

    public string? FullName { get; set; }

    public string? Position { get; set; }

    public decimal? Salary { get; set; }

    public int? ChiefId { get; set; }

    public int? RoleId { get; set; }

    public string? Login { get; set; }

    public string? PasswordHash { get; set; }

    public virtual Employee? Chief { get; set; }

    public virtual ICollection<DepartmentHead> DepartmentHeads { get; set; } = new List<DepartmentHead>();

    public virtual Department? Dept { get; set; }

    public virtual ICollection<Engineer> Engineers { get; set; } = new List<Engineer>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Employee> InverseChief { get; set; } = new List<Employee>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
