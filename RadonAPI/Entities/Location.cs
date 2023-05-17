using System;
using System.Collections.Generic;

namespace RadonAPI.Entities;

public partial class Location
{
    public int Id { get; set; }

    public int? RadonLoggerId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual RadonLogger? RadonLogger { get; set; }
}
