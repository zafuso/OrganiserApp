using OrganiserApp.Services;
using OrganiserApp.ViewModels;
using OrganiserApp.Views.Event;

namespace OrganiserApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
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

        builder.Services.AddSingleton<EventService>();
        builder.Services.AddSingleton<EventOverviewViewModel>();
        builder.Services.AddTransient<EventSettingsViewModel>();

        builder.Services.AddSingleton<EventOverviewPage>();
        builder.Services.AddTransient<EventSettingsPage>();

        return builder.Build();
	}
}
