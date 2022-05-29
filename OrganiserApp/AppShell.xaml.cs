using OrganiserApp.Views.Event;
using OrganiserApp.Views.Venue;

namespace OrganiserApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(TabBar), typeof(TabBar));
        Routing.RegisterRoute(nameof(EventSettingsPage), typeof(EventSettingsPage));
        Routing.RegisterRoute(nameof(EventPaymentMethodsPage), typeof(EventPaymentMethodsPage));
        Routing.RegisterRoute(nameof(CreateVenuePage), typeof(CreateVenuePage));
    }
}
