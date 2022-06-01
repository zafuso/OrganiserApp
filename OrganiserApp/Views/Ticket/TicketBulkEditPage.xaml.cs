using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Ticket;

public partial class TicketBulkEditPage : ContentPage
{
	public TicketBulkEditPage(TicketBulkEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as TicketBulkEditViewModel).Init();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
		Preferences.Remove("TicketList");
    }

    private void Picker_Focused(object sender, FocusEventArgs e)
    {
        Status.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Picker_Unfocused(object sender, FocusEventArgs e)
    {
        Status.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void OnlineFromDatePicker_Focused(object sender, FocusEventArgs e)
    {
        OnlineFromDate.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void OnlineFromDatePicker_Unfocused(object sender, FocusEventArgs e)
    {
        OnlineFromDate.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void OnlineUntilDatePicker_Focused(object sender, FocusEventArgs e)
    {
        OnlineUntilDate.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void OnlineUntilDatePicker_Unfocused(object sender, FocusEventArgs e)
    {
        OnlineUntilDate.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void OnlineFromTimePicker_Focused(object sender, FocusEventArgs e)
    {
        OnlineFromTime.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void OnlineFromTimePicker_Unfocused(object sender, FocusEventArgs e)
    {
        OnlineFromTime.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void OnlineUntilTimePicker_Focused(object sender, FocusEventArgs e)
    {
        OnlineUntilTime.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void OnlineUntilTimePicker_Unfocused(object sender, FocusEventArgs e)
    {
        OnlineUntilTime.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void ValidFromDatePicker_Focused(object sender, FocusEventArgs e)
    {
        ValidFromDate.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void ValidFromDatePicker_Unfocused(object sender, FocusEventArgs e)
    {
        ValidFromDate.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void ValidUntilDatePicker_Focused(object sender, FocusEventArgs e)
    {
        ValidUntilDate.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void ValidUntilDatePicker_Unfocused(object sender, FocusEventArgs e)
    {
        ValidUntilDate.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void ValidFromTimePicker_Focused(object sender, FocusEventArgs e)
    {
        ValidFromTime.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void ValidFromTimePicker_Unfocused(object sender, FocusEventArgs e)
    {
        ValidFromTime.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void ValidUntilTimePicker_Focused(object sender, FocusEventArgs e)
    {
        ValidUntilTime.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void ValidUntilTimePicker_Unfocused(object sender, FocusEventArgs e)
    {
        ValidUntilTime.BorderColor = Application.Current.Resources["Black10"] as Color;
    }
}