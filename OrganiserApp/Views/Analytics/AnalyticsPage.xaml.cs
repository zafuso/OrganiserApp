using LiveChartsCore.SkiaSharpView.Maui;
using LiveChartsCore.SkiaSharpView.SKCharts;
using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Analytics;

public partial class AnalyticsPage : ContentPage
{
    private readonly string _folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    public AnalyticsPage(AnalyticsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as AnalyticsViewModel).Init();
    }
}