using CommunityToolkit.Maui.Alerts;
using OrganiserApp.Helpers;
using OrganiserApp.Services;
using OrganiserApp.ViewModels;
using OrganiserApp.Views.Analytics;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Help;
using OrganiserApp.Views.Orders;
using OrganiserApp.Views.Shop;
using OrganiserApp.Views.Ticket;
using OrganiserApp.Views.Venue;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace OrganiserApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        // Temp workaround for a bug in community toolkit: https://github.com/CommunityToolkit/Maui/issues/427
        _ = new Snackbar();

		var builder = MauiApp.CreateBuilder();
		builder
            .UseSkiaSharp(true)
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("GothamRnd-Bold.otf", "Bold");
                fonts.AddFont("GothamRnd-BoldIta.otf", "BoldIta");
                fonts.AddFont("GothamRnd-Book.otf", "Book");
                fonts.AddFont("GothamRnd-BookIta.otf", "BookIta");
                fonts.AddFont("GothamRnd-Light.otf", "Light");
                fonts.AddFont("GothamRnd-LightIta.otf", "LightIta");
                fonts.AddFont("GothamRnd-MedIta.otf", "MedIta");
                fonts.AddFont("GothamRnd-Medium.otf", "Medium");
                fonts.AddFont("cm-icons.ttf", "Icons");
                fonts.AddFont("fa-brand-400.ttf", "FAB");
                fonts.AddFont("fa-regular-400.ttf", "FAR");
                fonts.AddFont("fa-solid-900.ttf", "FAS");
            });

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);
        builder.Services.AddSingleton<IEmail>(Email.Default);
        builder.Services.AddSingleton<IClipboard>(Clipboard.Default);

        builder.Services.AddSingleton<EventService>();
        builder.Services.AddSingleton<VenueService>();
        builder.Services.AddSingleton<CountryService>();
        builder.Services.AddSingleton<TicketService>();
        builder.Services.AddSingleton<ShopService>();
        builder.Services.AddSingleton<AnalyticsService>();
        builder.Services.AddSingleton<OrderService>();

        builder.Services.AddTransient<EventOverviewViewModel>();
        builder.Services.AddTransient<EventSettingsViewModel>();
        builder.Services.AddTransient<EventPaymentMethodsViewModel>();
        builder.Services.AddTransient<CreateVenueViewModel>();
        builder.Services.AddTransient<TicketOverviewViewModel>();
        builder.Services.AddTransient<TicketDetailsViewModel>();
        builder.Services.AddTransient<ShopOverviewViewModel>();
        builder.Services.AddTransient<OrderOverviewViewModel>();
        builder.Services.AddTransient<GuestListOverviewViewModel>();
        builder.Services.AddTransient<HelpViewModel>();
        builder.Services.AddTransient<AnalyticsViewModel>();
        builder.Services.AddTransient<TicketBulkEditViewModel>();
        builder.Services.AddTransient<TicketCategoryViewModel>();
        builder.Services.AddTransient<CreateShopViewModel>();
        builder.Services.AddTransient<ShopSettingsViewModel>();
        builder.Services.AddTransient<ShopBypassLinkViewModel>();
        builder.Services.AddTransient<ShopEditNameViewModel>();
        builder.Services.AddTransient<ShopTicketsViewModel>();
        builder.Services.AddTransient<ShopTicketsGroupViewModel>();
        builder.Services.AddTransient<OrderDetailsViewModel>();
        builder.Services.AddTransient<GuestListInvitationViewModel>();
        builder.Services.AddTransient<OrderEditCustomerDataViewModel>();

        builder.Services.AddTransient<EventOverviewPage>();
        builder.Services.AddTransient<EventSettingsPage>();
        builder.Services.AddTransient<EventPaymentMethodsPage>();
        builder.Services.AddTransient<CreateVenuePage>();
        builder.Services.AddTransient<TicketOverviewPage>();
        builder.Services.AddTransient<TicketDetailsPage>();
        builder.Services.AddTransient<ShopOverviewPage>();
        builder.Services.AddTransient<OrderOverviewPage>();
        builder.Services.AddTransient<GuestListOverviewPage>();
        builder.Services.AddTransient<AnalyticsPage>();
        builder.Services.AddTransient<HelpPage>();
        builder.Services.AddTransient<TicketBulkEditPage>();
        builder.Services.AddTransient<TicketCategoryPage>();
        builder.Services.AddTransient<CreateShopPage>();
        builder.Services.AddTransient<ShopSettingsPage>();
        builder.Services.AddTransient<ShopBypassLinkPage>();
        builder.Services.AddTransient<ShopEditNamePage>();
        builder.Services.AddTransient<OrderDetailsPage>();
        builder.Services.AddTransient<GuestListInvitationPage>();
        builder.Services.AddTransient<OrderEditCustomerDataPage>();

        return builder.Build();
	}
}
