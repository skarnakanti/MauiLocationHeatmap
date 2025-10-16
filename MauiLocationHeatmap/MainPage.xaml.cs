using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using MauiLocationHeatmap.Data;
using MauiLocationHeatmap.Models;

namespace MauiLocationHeatmap;

public partial class MainPage : ContentPage
{
    private readonly LocationDatabase _database = new();
    private bool _tracking;

    public MainPage()
    {
        InitializeComponent();
        InitializeMap();
    }

    private async void InitializeMap()
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                var mapSpan = MapSpan.FromCenterAndRadius(
                    new Location(location.Latitude, location.Longitude),
                    Distance.FromKilometers(1));
                map.MoveToRegion(mapSpan);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to get location: {ex.Message}", "OK");
        }
    }

    private async void OnStartTrackingClicked(object sender, EventArgs e)
    {
        _tracking = !_tracking;
        ((Button)sender).Text = _tracking ? "Stop Tracking" : "Track Location";

        if (_tracking)
        {
            while (_tracking)
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    var record = new LocationRecord
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Timestamp = DateTime.UtcNow
                    };

                    await _database.SaveLocationAsync(record);
                    DrawHeatMap();
                }

                await Task.Delay(5000);
            }
        }
    }

    private async void DrawHeatMap()
    {
        map.Pins.Clear();

        var locations = await _database.GetLocationsAsync();

        foreach (var loc in locations)
        {
            // Use a Pin instead of MapCircle
            var pin = new Pin
            {
                Label = "Tracked",
                Location = new Location(loc.Latitude, loc.Longitude),
                Type = PinType.Place
            };

            map.Pins.Add(pin);
        }
    }
}
