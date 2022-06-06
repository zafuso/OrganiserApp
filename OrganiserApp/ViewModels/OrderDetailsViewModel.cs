using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Orders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedOrder", "SelectedOrder")]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        string EventUuid;
        [ObservableProperty]
        Order selectedOrder;
        [ObservableProperty]
        bool isValidOrder;

        private readonly OrderService orderService;

        public OrderDetailsViewModel(OrderService orderService)
        {
            this.orderService = orderService;
        }

        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            Title = $"Order {SelectedOrder.BatchId}";

            if (SelectedOrder.Status == Enums.OrderStatusType.CANCELED || 
                SelectedOrder.Status == Enums.OrderStatusType.RELEASED ||
                SelectedOrder.Status == Enums.OrderStatusType.PENDING ||
                SelectedOrder.Status == Enums.OrderStatusType.REFUNDED)
            {
                IsValidOrder = false;
            } 
            else
            {
                IsValidOrder = true;
            }
        }

        [ICommand]
        async Task DownloadOrderAsync()
        {
            if (IsBusy || SelectedOrder is null)
                return;

            try
            {
                var isConfirmed = await Shell.Current.DisplayAlert("Demo Order PDF", $"Your downloadlink is: {SelectedOrder.DownloadUrl}. As this application is not running on production we'll send you to a demo order instead.", "OK", "Cancel");

                if (!isConfirmed)
                    return;

                IsBusy = true;

                Uri demoUri = new("https://order.ticketing.cm.com/CF29902358/3e86d820-96b2-40f2-a51e-e7d3b7453e2b/b5dabb277fc7e00ae1ddc99676b0d7585a2c62d24b8512628fabd407dbe0eba89f835e2d38e32695c0665f824fe12db655127d3515dbde041cdf1bb4e5c57698");
                await Browser.Default.OpenAsync(demoUri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to open browser: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task CancelOrderAsync()
        {
            if (IsBusy || SelectedOrder is null)
                return;

            try
            {
                IsBusy = true;

                var isConfirmed = await Shell.Current.DisplayAlert("Cancel order", $"Are you sure you want to cancel order {SelectedOrder.BatchId}? Tickets will no longer be valid!", "OK", "Cancel");

                if (!isConfirmed)
                    return;

                await orderService.CancelOrderAsync(EventUuid, SelectedOrder);
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(OrderOverviewPage)}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to cancel order: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
