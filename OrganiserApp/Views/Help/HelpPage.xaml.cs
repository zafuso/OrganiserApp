using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Help;

public partial class HelpPage : ContentPage
{
	public HelpPage(HelpViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}