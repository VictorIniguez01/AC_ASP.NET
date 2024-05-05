using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Resident
{
    public int ResidentId { get; set; }

    public string? ResidentName { get; set; }

    public string? ResidentLastName { get; set; }

    public int? ResidentPhone { get; set; }

    public int? HouseId { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual House? House { get; set; }
}
