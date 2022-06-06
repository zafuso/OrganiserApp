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
}