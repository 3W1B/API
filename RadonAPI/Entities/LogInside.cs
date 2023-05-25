using System;
using System.Collections.Generic;

namespace RadonAPI.Entities;

public partial class LogInside
{
    public int Id { get; set; }

    public int? LogId { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }

    public double RadonLta { get; set; }

    public double RadonSta { get; set; }
}
