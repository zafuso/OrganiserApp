using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Venue;

public partial class CreateVenuePage : ContentPage
{
	public CreateVenuePage(CreateVenueViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as CreateVenueViewModel).Init();
    }

    private void Name_Focused(object sender, FocusEventArgs e)
    {
        Name.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Name_Unfocused(object sender, FocusEventArgs e)
    {
        Name.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Picker_Focused(object sender, FocusEventArgs e)
    {
        Country.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Picker_Unfocused(object sender, FocusEventArgs e)
    {
        Country.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Street_Focused(object sender, FocusEventArgs e)
    {
        Street.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Street_Unfocused(object sender, FocusEventArgs e)
    {
        Street.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Zipcode_Focused(object sender, FocusEventArgs e)
    {
        Zipcode.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Zipcode_Unfocused(object sender, FocusEventArgs e)
    {
        Zipcode.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void City_Focused(object sender, FocusEventArgs e)
    {
        City.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void City_Unfocused(object sender, FocusEventArgs e)
    {
        City.BorderColor = Application.Current.Resources["Black10"] as Color;
    }
}