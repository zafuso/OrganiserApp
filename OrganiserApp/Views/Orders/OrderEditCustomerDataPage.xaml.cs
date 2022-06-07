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
            (BindingContext as OrderEditCustomerDataViewModel).IsValidFirstName = false;
        }

        if (FirstNameValidator.IsValid)
        {
            FirstName.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidFirstName = true;
        }

        (BindingContext as OrderEditCustomerDataViewModel).ValidateForm();
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
            (BindingContext as OrderEditCustomerDataViewModel).IsValidLastName = false;
        }

        if (LastNameValidator.IsValid)
        {
            LastName.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidLastName = true;
        }

        (BindingContext as OrderEditCustomerDataViewModel).ValidateForm();
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
            (BindingContext as OrderEditCustomerDataViewModel).IsValidEmail = false;
        }

        if (EmailValidator.IsValid)
        {
            Email.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidEmail = true;
        }

        (BindingContext as OrderEditCustomerDataViewModel).ValidateForm();
    }
    private void Mobile_Focused(object sender, FocusEventArgs e)
    {
        Mobile.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Mobile_Unfocused(object sender, FocusEventArgs e)
    {
        Mobile.BorderColor = Application.Current.Resources["Black10"] as Color;

        MobileValidator.ForceValidate();

        if (MobileValidator.IsNotValid)
        {
            Mobile.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidMobile = false;
        }

        if (MobileValidator.IsValid)
        {
            Mobile.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidMobile = true;
        }

        (BindingContext as OrderEditCustomerDataViewModel).ValidateForm();
    }

    private void Street_Focused(object sender, FocusEventArgs e)
    {
        Street.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Street_Unfocused(object sender, FocusEventArgs e)
    {
        Street.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void HouseNumber_Focused(object sender, FocusEventArgs e)
    {
        HouseNumber.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void HouseNumber_Unfocused(object sender, FocusEventArgs e)
    {
        HouseNumber.BorderColor = Application.Current.Resources["Black10"] as Color;
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

    private void Picker_Focused(object sender, FocusEventArgs e)
    {
        Country.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Picker_Unfocused(object sender, FocusEventArgs e)
    {
        Country.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Gender_Focused(object sender, FocusEventArgs e)
    {
        Gender.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Gender_Unfocused(object sender, FocusEventArgs e)
    {
        Gender.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Email_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        EmailValidator.ForceValidate();

        if (EmailValidator.IsNotValid)
        {
            Email.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidEmail = false;
        }

        if (EmailValidator.IsValid)
        {
            Email.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidEmail = true;
        }

        (BindingContext as OrderEditCustomerDataViewModel).ValidateForm();
    }

    private void FirstName_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        FirstNameValidator.ForceValidate();

        if (FirstNameValidator.IsNotValid)
        {
            FirstName.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidFirstName = false;
        }

        if (FirstNameValidator.IsValid)
        {
            FirstName.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidFirstName = true;
        }

        (BindingContext as OrderEditCustomerDataViewModel).ValidateForm();
    }

    private void LastName_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        LastNameValidator.ForceValidate();

        if (LastNameValidator.IsNotValid)
        {
            LastName.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidLastName = false;
        }

        if (LastNameValidator.IsValid)
        {
            LastName.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidLastName = true;
        }

        (BindingContext as OrderEditCustomerDataViewModel).ValidateForm();
    }

    private void Mobile_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        MobileValidator.ForceValidate();

        if (MobileValidator.IsNotValid)
        {
            Mobile.BorderColor = Application.Current.Resources["Red100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidMobile = false;
        }

        if (MobileValidator.IsValid)
        {
            Mobile.BorderColor = Application.Current.Resources["Green100"] as Color;
            (BindingContext as OrderEditCustomerDataViewModel).IsValidMobile = true;
        }

        (BindingContext as OrderEditCustomerDataViewModel).ValidateForm();
    }
}