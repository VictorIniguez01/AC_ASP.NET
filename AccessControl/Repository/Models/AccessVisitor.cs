using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class AccessVisitor
{
    public int AccessVisitorId { get; set; }

    public DateTime? AccessVisitorEntry { get; set; }

    public int? VisitorId { get; set; }

    public bool? IsEntry { get; set; }

    public bool? IsGoingZone { get; set; }
}
