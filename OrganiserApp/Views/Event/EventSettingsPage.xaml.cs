using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Event;

public partial class EventSettingsPage : ContentPage
{
	public EventSettingsPage(EventSettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}