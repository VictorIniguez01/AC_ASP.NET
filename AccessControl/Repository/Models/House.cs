using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class House
{
    public int HouseId { get; set; }

    public string? HouseStreet { get; set; }

    public int? ZoneId { get; set; }

    public int? HouseNumber { get; set; }

    public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();

    public virtual ICollection<Visitor> Visitors { get; set; } = new List<Visitor>();

    public virtual Zone? Zone { get; set; }
}
