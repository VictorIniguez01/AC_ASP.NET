using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class AccessResident
{
    public int AccessResidentId { get; set; }

    public DateTime? AccessResidentEntry { get; set; }

    public int? ResidentId { get; set; }

    public virtual Resident? Resident { get; set; }
}
