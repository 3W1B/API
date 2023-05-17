using System;
using System.Collections.Generic;

namespace RadonAPI.Entities;

public partial class RadonLogger
{
    public int Id { get; set; }

    public string? Ip { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
