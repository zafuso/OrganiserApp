using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Event;

public partial class EventOverviewPage : ContentPage
{
	public EventOverviewPage(EventOverviewViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        ButtonAll.BackgroundColor = Color.FromRgb(255, 255, 255);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as EventOverviewViewModel).Init();
    }

    private void Button_All_Clicked(object sender, EventArgs e)
    {
        ButtonAll.BackgroundColor = Color.FromRgb(255, 255, 255);
        ButtonToday.BackgroundColor = Color.FromArgb("#EAEBEE");
        ButtonYesterday.BackgroundColor = Color.FromArgb("#EAEBEE");
    }

    private void Button_Today_Clicked(object sender, EventArgs e)
    {
        ButtonAll.BackgroundColor = Color.FromArgb("#EAEBEE");
        ButtonToday.BackgroundColor = Color.FromRgb(255, 255, 255);
        ButtonYesterday.BackgroundColor = Color.FromArgb("#EAEBEE");
    }

    private void Button_Yesterday_Clicked(object sender, EventArgs e)
    {
        ButtonAll.BackgroundColor = Color.FromArgb("#EAEBEE");
        ButtonToday.BackgroundColor = Color.FromArgb("#EAEBEE");
        ButtonYesterday.BackgroundColor = Color.FromRgb(255, 255, 255);
    }
}

