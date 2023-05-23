namespace RadonAPI.Entities;

public class LogOutside
{
    public int Id { get; set; }

    public int? LogId { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }
}