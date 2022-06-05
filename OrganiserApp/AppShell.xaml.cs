using OrganiserApp.Views.Analytics;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Help;
using OrganiserApp.Views.Orders;
using OrganiserApp.Views.Shop;
using OrganiserApp.Views.Ticket;
using OrganiserApp.Views.Venue;

namespace OrganiserApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(TabBar), typeof(TabBar));
        Routing.RegisterRoute(nameof(EventOverviewPage), typeof(EventOverviewPage));
        Routing.RegisterRoute(nameof(EventSettingsPage), typeof(EventSettingsPage));
        Routing.RegisterRoute(nameof(EventPaymentMethodsPage), typeof(EventPaymentMethodsPage));
        Routing.RegisterRoute(nameof(CreateVenuePage), typeof(CreateVenuePage));
        Routing.RegisterRoute(nameof(AnalyticsPage), typeof(AnalyticsPage));
        Routing.RegisterRoute(nameof(HelpPage), typeof(HelpPage));
        Routing.RegisterRoute(nameof(OrderOverviewPage), typeof(OrderOverviewPage));
        Routing.RegisterRoute(nameof(GuestListOverviewPage), typeof(GuestListOverviewPage));
        Routing.RegisterRoute(nameof(ShopOverviewPage), typeof(ShopOverviewPage));
        Routing.RegisterRoute(nameof(TicketOverviewPage), typeof(TicketOverviewPage));
        Routing.RegisterRoute(nameof(TicketDetailsPage), typeof(TicketDetailsPage));
        Routing.RegisterRoute(nameof(TicketBulkEditPage), typeof(TicketBulkEditPage));
        Routing.RegisterRoute(nameof(TicketCategoryPage), typeof(TicketCategoryPage));
        Routing.RegisterRoute(nameof(CreateShopPage), typeof(CreateShopPage));
        Routing.RegisterRoute(nameof(ShopSettingsPage), typeof(ShopSettingsPage));
        Routing.RegisterRoute(nameof(ShopBypassLinkPage), typeof(ShopBypassLinkPage));
        Routing.RegisterRoute(nameof(ShopEditNamePage), typeof(ShopEditNamePage));
    }
}
