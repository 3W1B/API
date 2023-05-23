namespace RadonAPI.Entities;

public class Logger
{
    public string Id { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}