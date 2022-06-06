using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Orders;

public partial class OrderOverviewPage : ContentPage
{
	public OrderOverviewPage(OrderOverviewViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as OrderOverviewViewModel).Init();
    }
}