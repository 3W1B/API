using System;
using System.Collections.Generic;

namespace RadonAPI.Entities;

public partial class UserRadonLogger
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RadonLoggerId { get; set; }
}
