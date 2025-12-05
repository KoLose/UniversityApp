using System;
using System.Collections.Generic;

namespace University.Data;

public partial class SpecialtySubject
{
    public int SpecialtySubjectId { get; set; }

    public int? SpecialtyId { get; set; }

    public int? SubjectId { get; set; }

    public virtual Specialty? Specialty { get; set; }

    public virtual Subject? Subject { get; set; }
}
