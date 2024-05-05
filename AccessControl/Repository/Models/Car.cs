using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Car
{
    public int CarId { get; set; }

    public string? CarBrand { get; set; }

    public string? CarModel { get; set; }

    public string? CarPlate { get; set; }

    public string? CarColor { get; set; }

    public virtual ICollection<Visitor> Visitors { get; set; } = new List<Visitor>();
}
