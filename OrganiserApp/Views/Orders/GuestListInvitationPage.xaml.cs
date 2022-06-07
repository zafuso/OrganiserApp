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

        FirstNameValidator.ForceValidate();

        if (FirstNameValidator.IsNotValid)
        {
            FirstName.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidFirstName = false;
        }

        if (FirstNameValidator.IsValid)
        {
            FirstName.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidFirstName = true;
        }

        (BindingContext as GuestListInvitationViewModel).ValidateForm();
    }

    private void LastName_Focused(object sender, FocusEventArgs e)
    {
        LastName.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void LastName_Unfocused(object sender, FocusEventArgs e)
    {
        LastName.BorderColor = Application.Current.Resources["Black10"] as Color;

        LastNameValidator.ForceValidate();

        if (LastNameValidator.IsNotValid)
        {
            LastName.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidLastName = false;
        }

        if (LastNameValidator.IsValid)
        {
            LastName.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidLastName = true;
        }

        (BindingContext as GuestListInvitationViewModel).ValidateForm();
    }

    private void Note_Focused(object sender, FocusEventArgs e)
    {
        Note.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Note_Unfocused(object sender, FocusEventArgs e)
    {
        Note.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Picker_Focused(object sender, FocusEventArgs e)
    {
        Language.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Picker_Unfocused(object sender, FocusEventArgs e)
    {
        Language.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Email_Focused(object sender, FocusEventArgs e)
    {
        Email.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Email_Unfocused(object sender, FocusEventArgs e)
    {
        Email.BorderColor = Application.Current.Resources["Black10"] as Color;

        EmailValidator.ForceValidate();

        if (EmailValidator.IsNotValid)
        {
            Email.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidEmail = false;
        }

        if (EmailValidator.IsValid)
        {
            Email.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidEmail = true;
        }

        (BindingContext as GuestListInvitationViewModel).ValidateForm();
    }

    private void Email_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        EmailValidator.ForceValidate();

        if (EmailValidator.IsNotValid)
        {
            Email.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidEmail = false;
        }

        if (EmailValidator.IsValid)
        {
            Email.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidEmail = true;
        }

        (BindingContext as GuestListInvitationViewModel).ValidateForm();
    }

    private void LastName_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        LastNameValidator.ForceValidate();

        if (LastNameValidator.IsNotValid)
        {
            LastName.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidLastName = false;
        }

        if (LastNameValidator.IsValid)
        {
            LastName.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidLastName = true;
        }

        (BindingContext as GuestListInvitationViewModel).ValidateForm();
    }

    private void FirstName_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        FirstNameValidator.ForceValidate();

        if (FirstNameValidator.IsNotValid)
        {
            FirstName.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidFirstName = false;
        }

        if (FirstNameValidator.IsValid)
        {
            FirstName.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as GuestListInvitationViewModel).IsValidFirstName = true;
        }

        (BindingContext as GuestListInvitationViewModel).ValidateForm();
    }
}