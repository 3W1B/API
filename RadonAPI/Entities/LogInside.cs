namespace RadonAPI.Entities;

public class LogInside
{
    public int Id { get; set; }

    public int? LogId { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }

    public double Radon { get; set; }
}