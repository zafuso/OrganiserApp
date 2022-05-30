using OrganiserApp.ViewModels;

namespace OrganiserApp.Views.Analytics;

public partial class TicketSalesAnalyticsPage : ContentPage
{
	public TicketSalesAnalyticsPage(TicketSalesAnalyticsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}