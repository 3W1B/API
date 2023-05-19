using System;
using System.Collections.Generic;

namespace RadonAPI.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<UserRadonLogger> UserRadonLoggers { get; set; } = new List<UserRadonLogger>();
}
