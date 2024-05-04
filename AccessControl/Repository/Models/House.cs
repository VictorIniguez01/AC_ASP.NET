using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class House
{
    public int HouseId { get; set; }

    public string? HouseStreet { get; set; }

    public int? ZoneId { get; set; }

    public int? HouseNumber { get; set; }

    public virtual ICollection<AccessVisitor> AccessVisitors { get; set; } = new List<AccessVisitor>();

    public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();

    public virtual Zone? Zone { get; set; }
}
