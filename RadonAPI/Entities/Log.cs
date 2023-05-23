namespace RadonAPI.Entities;

public class Log
{
    public int Id { get; set; }

    public string? LoggerId { get; set; }

    public DateTime Timestamp { get; set; }

    public virtual ICollection<LogInside> LogInsides { get; set; } = new List<LogInside>();

    public virtual ICollection<LogOutside> LogOutsides { get; set; } = new List<LogOutside>();
}