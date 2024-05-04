using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Card
{
    public int CardId { get; set; }

    public string? CardCode { get; set; }

    public int? ResidentId { get; set; }

    public virtual Resident? Resident { get; set; }
}
