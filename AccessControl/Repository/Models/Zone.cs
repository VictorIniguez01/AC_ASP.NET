using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Zone
{
    public int ZoneId { get; set; }

    public string? ZoneName { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual ICollection<House> Houses { get; set; } = new List<House>();
}
