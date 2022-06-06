using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Orders;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage(OrderDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		(BindingContext as OrderDetailsViewModel).Init();
    }
}