using OrganiserApp.ViewModels;
using System.Diagnostics;

namespace OrganiserApp.Views.Shop;

public partial class ShopSettingsPage : ContentPage
{
	public ShopSettingsPage(ShopSettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		(BindingContext as ShopSettingsViewModel).Init();
    }

    private void DragGestureRecognizer_DragStarting(Object sender, DragStartingEventArgs e)
    {
        var label = (Label)((Element)sender).Parent;
        Debug.WriteLine($"DragGestureRecognizer_DragStarting [{label.Text}]");

        e.Data.Properties["Label"] = label;

        //e.Handled = true;
    }

    private void DropGestureRecognizer_Drop(Object sender, DropEventArgs e)
    {
        var label = (Label)((Element)sender).Parent;
        var dropLabel = (Label)e.Data.Properties["Label"];
        if (label == dropLabel)
            return;

        Debug.WriteLine($"DropGestureRecognizer_Drop [{dropLabel.Text}] => [{label.Text}]");

        var sourceContainer = (Grid)dropLabel.Parent;
        var targetContainer = (Grid)label.Parent;
        sourceContainer.Children.Remove(dropLabel);
        targetContainer.Children.Remove(label);
        sourceContainer.Children.Add(label);
        targetContainer.Children.Add(dropLabel);

        e.Handled = true;
    }

    private void DragGestureRecognizer_DragStarting_Collection(System.Object sender, DragStartingEventArgs e)
    {

    }

    private void DropGestureRecognizer_Drop_Collection(System.Object sender, DropEventArgs e)
    {
        // We handle reordering login in our view model
        e.Handled = true;
    }
}