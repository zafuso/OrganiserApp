using OrganiserApp.Views.Event;

namespace OrganiserApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(EventSettingsPage), typeof(EventSettingsPage));
    }
}
