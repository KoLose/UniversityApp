using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? Code { get; set; }

    public int? Hours { get; set; }

    public string? Name { get; set; }

    public int? ExecutorDeptId { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual Department? ExecutorDept { get; set; }

    public virtual ICollection<SpecialtySubject> SpecialtySubjects { get; set; } = new List<SpecialtySubject>();
}
