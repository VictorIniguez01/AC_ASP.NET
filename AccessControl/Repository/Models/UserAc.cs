using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class UserAc
{
    public int UserAcId { get; set; }

    public string? UserAcName { get; set; }

    public string? UserAcPassword { get; set; }

    public DateTime? UserAcLastLogin { get; set; }

    public string? UserAcMqttTopic { get; set; }

    public bool? UserAcIsZone { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
