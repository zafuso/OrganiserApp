using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Ticket;

public partial class TicketDetailsPage : ContentPage
{
	public TicketDetailsPage(TicketDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}