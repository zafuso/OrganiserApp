using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Shop;

public partial class ShopSettingsPage : ContentPage
{
	public ShopSettingsPage(ShopSettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		(BindingContext as ShopSettingsViewModel).Init();
    }
}