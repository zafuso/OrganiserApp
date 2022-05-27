using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Event;

public partial class EventSettingsPage : ContentPage
{
	public EventSettingsPage(EventSettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    private void Name_Focused(object sender, FocusEventArgs e)
    {
        NameFrame.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Description_Focused(object sender, FocusEventArgs e)
    {

    }
}