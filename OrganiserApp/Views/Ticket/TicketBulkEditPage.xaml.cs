using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Ticket;

public partial class TicketBulkEditPage : ContentPage
{
	public TicketBulkEditPage(TicketBulkEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as TicketBulkEditViewModel).Init();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
		Preferences.Remove("TicketList");
    }
}