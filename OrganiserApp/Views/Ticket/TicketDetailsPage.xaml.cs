using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Ticket;

public partial class TicketDetailsPage : ContentPage
{
	public TicketDetailsPage(TicketDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as TicketDetailsViewModel).Init();
    }

    private void Button_Existing_Clicked(object sender, EventArgs e)
    {
        ButtonExisting.BackgroundColor = Color.FromArgb("#E5F2FF");
        ButtonExisting.BorderColor = Color.FromArgb("#0074E8");
        ButtonExisting.TextColor = Color.FromArgb("#0074E8");
        ButtonNew.TextColor = Color.FromArgb("#101E1E");
        ButtonNew.BorderColor = Color.FromArgb("#00FFFFFF");
        ButtonNew.BackgroundColor = Color.FromArgb("#00FFFFFF");

    }

    private void Button_New_Clicked(object sender, EventArgs e)
    {
        ButtonNew.BackgroundColor = Color.FromArgb("#E5F2FF");
        ButtonNew.BorderColor = Color.FromArgb("0074E8");
        ButtonNew.TextColor = Color.FromArgb("#0074E8");
        ButtonExisting.TextColor = Color.FromArgb("#101E1E");
        ButtonExisting.BorderColor = Color.FromArgb("#00FFFFFF");
        ButtonExisting.BackgroundColor = Color.FromArgb("#00FFFFFF");
    }

    private void Picker_Focused(object sender, FocusEventArgs e)
    {
        Status.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Picker_Unfocused(object sender, FocusEventArgs e)
    {
        Status.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void PickerPool_Focused(object sender, FocusEventArgs e)
    {
        Ticketpool.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void PickerPool_Unfocused(object sender, FocusEventArgs e)
    {
        Ticketpool.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Name_Focused(object sender, FocusEventArgs e)
    {
        Name.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Description_Focused(object sender, FocusEventArgs e)
    {
        Description.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Name_Unfocused(object sender, FocusEventArgs e)
    {
        Name.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Description_Unfocused(object sender, FocusEventArgs e)
    {
        Description.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Subtitle_Focused(object sender, FocusEventArgs e)
    {
        Subtitle.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Subtitle_Unfocused(object sender, FocusEventArgs e)
    {
        Subtitle.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Capacity_Focused(object sender, FocusEventArgs e)
    {
        Capacity.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Capacity_Unfocused(object sender, FocusEventArgs e)
    {
        Capacity.BorderColor = Application.Current.Resources["Black10"] as Color;
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