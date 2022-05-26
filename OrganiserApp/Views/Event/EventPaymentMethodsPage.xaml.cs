using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Event;

public partial class EventPaymentMethodsPage : ContentPage
{
	public EventPaymentMethodsPage(EventPaymentMethodsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}