using Microsoft.Maui.Controls;

namespace MauiLocationHeatmap;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Proper initialization method for .NET 9 MAUI
        return new Window(new NavigationPage(new MainPage()));
    }
}