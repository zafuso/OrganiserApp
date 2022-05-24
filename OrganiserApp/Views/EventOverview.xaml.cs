namespace OrganiserApp.Views;

public partial class EventOverviewPage : ContentPage
{
	public EventOverviewPage()
	{
        InitializeComponent();
		ButtonAll.BackgroundColor = Color.FromRgb(255,255,255);
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

