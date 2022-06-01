using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Ticket;

public partial class TicketCategoryPage : ContentPage
{
	public TicketCategoryPage(TicketCategoryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as TicketCategoryViewModel).Init();
    }

    private void Title_Focused(object sender, FocusEventArgs e)
    {
        Title.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Description_Focused(object sender, FocusEventArgs e)
    {
        Description.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Title_Unfocused(object sender, FocusEventArgs e)
    {
        Title.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Description_Unfocused(object sender, FocusEventArgs e)
    {
        Description.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void MaxOrderAmount_Focused(object sender, FocusEventArgs e)
    {
        MaxOrderAmount.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void MaxOrderAmount_Unfocused(object sender, FocusEventArgs e)
    {
        MaxOrderAmount.BorderColor = Application.Current.Resources["Black10"] as Color;
    }
}