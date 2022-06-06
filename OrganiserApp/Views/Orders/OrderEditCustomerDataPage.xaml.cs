using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Orders;

public partial class OrderEditCustomerDataPage : ContentPage
{
	public OrderEditCustomerDataPage(OrderEditCustomerDataViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		(BindingContext as OrderEditCustomerDataViewModel).Init();
    }
}