using SQLite;
using MauiLocationHeatmap.Models;

namespace MauiLocationHeatmap.Data;

public class LocationDatabase
{
    private readonly SQLiteAsyncConnection _db;

    public LocationDatabase()
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "locations.db3");

        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<LocationRecord>().Wait();
    }

    public Task<List<LocationRecord>> GetLocationsAsync() =>
        _db.Table<LocationRecord>().ToListAsync();

    public Task<int> SaveLocationAsync(LocationRecord record) =>
        _db.InsertAsync(record);
}