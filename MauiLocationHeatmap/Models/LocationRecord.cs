using SQLite;

namespace MauiLocationHeatmap.Models;

public class LocationRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Add Timestamp property
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}