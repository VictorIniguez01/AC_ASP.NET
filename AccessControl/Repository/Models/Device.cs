using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Device
{
    public int DeviceId { get; set; }

    public string? DeviceName { get; set; }

    public int? ZoneId { get; set; }

    public int? UserAcId { get; set; }

    public virtual UserAc? UserAc { get; set; }

    public virtual Zone? Zone { get; set; }
}
