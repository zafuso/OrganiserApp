using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Shop;

public partial class ShopEditNamePage : ContentPage
{
    public ShopEditNamePage(ShopEditNameViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as ShopEditNameViewModel).Init();
    }

    private void Shoplink_Focused(object sender, FocusEventArgs e)
    {
        Shoplink.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Shoplink_Unfocused(object sender, FocusEventArgs e)
    {
        Shoplink.BorderColor = Application.Current.Resources["Black10"] as Color;
    }
}