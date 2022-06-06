using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Orders;

public partial class GuestListInvitationPage : ContentPage
{
	public GuestListInvitationPage(GuestListInvitationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		(BindingContext as GuestListInvitationViewModel).Init();
    }

    private void FirstName_Focused(object sender, FocusEventArgs e)
    {
        FirstName.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void FirstName_Unfocused(object sender, FocusEventArgs e)
    {
        FirstName.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void LastName_Focused(object sender, FocusEventArgs e)
    {
        LastName.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void LastName_Unfocused(object sender, FocusEventArgs e)
    {
        LastName.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Note_Focused(object sender, FocusEventArgs e)
    {
        Note.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Note_Unfocused(object sender, FocusEventArgs e)
    {
        Note.BorderColor = Application.Current.Resources["Black10"] as Color;
    }
}