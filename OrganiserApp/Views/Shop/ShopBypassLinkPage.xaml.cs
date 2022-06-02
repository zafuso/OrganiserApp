using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Shop;

public partial class ShopBypassLinkPage : ContentPage
{
	public ShopBypassLinkPage(ShopBypassLinkViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		(BindingContext as ShopBypassLinkViewModel).Init();
    }
}