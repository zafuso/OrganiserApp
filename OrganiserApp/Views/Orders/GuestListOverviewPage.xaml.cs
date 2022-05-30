using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Orders;

public partial class GuestListOverviewPage : ContentPage
{
	public GuestListOverviewPage(GuestListOverviewViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}