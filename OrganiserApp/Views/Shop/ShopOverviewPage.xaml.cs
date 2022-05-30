using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Shop;

public partial class ShopOverviewPage : ContentPage
{
	public ShopOverviewPage(ShopOverviewViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}