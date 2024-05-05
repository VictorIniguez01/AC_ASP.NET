using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string? VisitorName { get; set; }

    public string? VisitorLastName { get; set; }

    public int? CarId { get; set; }

    public int? HouseId { get; set; }

    public virtual Car? Car { get; set; }

    public virtual House? House { get; set; }
}
