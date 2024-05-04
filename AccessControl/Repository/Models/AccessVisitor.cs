using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class AccessVisitor
{
    public int AccessVisitorId { get; set; }

    public DateTime? AccessVisitorEntry { get; set; }

    public int? HouseId { get; set; }

    public int? CarId { get; set; }

    public int? VisitorId { get; set; }

    public virtual Car? Car { get; set; }

    public virtual House? House { get; set; }

    public virtual Visitor? Visitor { get; set; }
}
