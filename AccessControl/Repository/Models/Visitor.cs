using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string? VisitorName { get; set; }

    public string? VisitorLastName { get; set; }

    public virtual ICollection<AccessVisitor> AccessVisitors { get; set; } = new List<AccessVisitor>();
}
