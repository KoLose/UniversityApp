using System;
using System.Collections.Generic;

namespace University.Data;

public partial class Exam
{
    public int ExamId { get; set; }

    public DateOnly? ExamDate { get; set; }

    public int? SubjectId { get; set; }

    public int? StudentId { get; set; }

    public int? TeacherId { get; set; }

    public string? Auditorium { get; set; }

    public int? Grade { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual Employee? Teacher { get; set; }
}
