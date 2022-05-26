using OrganiserApp.Views.Event;

namespace OrganiserApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(TabBar), typeof(TabBar));
        Routing.RegisterRoute(nameof(EventSettingsPage), typeof(EventSettingsPage));
        Routing.RegisterRoute(nameof(EventPaymentMethodsPage), typeof(EventPaymentMethodsPage));
    }
}
