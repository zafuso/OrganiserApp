using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Ticket;

public partial class TicketOverviewPage : ContentPage
{
	public TicketOverviewPage(TicketOverviewViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as TicketOverviewViewModel).Init();
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {

    }
}