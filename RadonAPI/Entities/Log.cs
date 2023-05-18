using System;
using System.Collections.Generic;

namespace RadonAPI.Entities;

public partial class Log
{
    public int Id { get; set; }

    public int? RadonLoggerId { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual ICollection<LogInside> LogInsides { get; set; } = new List<LogInside>();

    public virtual ICollection<LogOutside> LogOutsides { get; set; } = new List<LogOutside>();
}
