namespace RadonAPI.Entities;

public class Location
{
    public int Id { get; set; }

    public string? LoggerId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}