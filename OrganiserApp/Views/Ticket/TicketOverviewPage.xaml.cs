using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Ticket;

public partial class TicketOverviewPage : ContentPage
{
	public TicketOverviewPage(TicketOverviewViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}