using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Event;

public partial class EventSettingsPage : ContentPage
{
	public EventSettingsPage(EventSettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as EventSettingsViewModel).Init();
    }

    private void Name_Focused(object sender, FocusEventArgs e)
    {
        Name.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Description_Focused(object sender, FocusEventArgs e)
    {
        Description.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void Picker_Focused(object sender, FocusEventArgs e)
    {
        Location.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void EndAtTimePicker_Focused(object sender, FocusEventArgs e)
    {
        EndAtTime.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void EndAtDatePicker_Focused(object sender, FocusEventArgs e)
    {
        EndAtDate.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void StartAtTimePicker_Focused(object sender, FocusEventArgs e)
    {
        StartAtTime.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void StartAtDatePicker_Focused(object sender, FocusEventArgs e)
    {
        StartAtDate.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void StartAtDatePicker_Unfocused(object sender, FocusEventArgs e)
    {
        StartAtDate.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void StartAtTimePicker_Unfocused(object sender, FocusEventArgs e)
    {
        StartAtTime.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void EndAtDatePicker_Unfocused(object sender, FocusEventArgs e)
    {
        EndAtDate.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void EndAtTimePicker_Unfocused(object sender, FocusEventArgs e)
    {
        EndAtTime.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Name_Unfocused(object sender, FocusEventArgs e)
    {
        Name.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Description_Unfocused(object sender, FocusEventArgs e)
    {
        Description.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void Picker_Unfocused(object sender, FocusEventArgs e)
    {
        Location.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void MinutesToOrder_Unfocused(object sender, FocusEventArgs e)
    {
        MinutesToOrder.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void MinutesToOrder_Focused(object sender, FocusEventArgs e)
    {
        MinutesToOrder.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void PurchaseOrderNumber_Unfocused(object sender, FocusEventArgs e)
    {
        PurchaseOrderNumber.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void PurchaseOrderNumber_Focused(object sender, FocusEventArgs e)
    {
        PurchaseOrderNumber.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void DownloadFromDate_Unfocused(object sender, FocusEventArgs e)
    {
        DownloadFromDate.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void DownloadFromDate_Focused(object sender, FocusEventArgs e)
    {
        DownloadFromDate.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }

    private void DownloadFromTime_Unfocused(object sender, FocusEventArgs e)
    {
        DownloadFromTime.BorderColor = Application.Current.Resources["Black10"] as Color;
    }

    private void DownloadFromTime_Focused(object sender, FocusEventArgs e)
    {
        DownloadFromTime.BorderColor = Application.Current.Resources["Blue110"] as Color;
    }
}