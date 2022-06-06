using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using OrganiserApp.Helpers;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    public partial class GuestListOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<Order> OrderList { get; set; } = new();

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        public int totalItems;
        [ObservableProperty]
        bool canLoadMore;
        [ObservableProperty]
        int ordersCount;

        string EventUuid;
        int skip;
        int take = 15;
        bool LoadMore;
        private readonly OrderService orderService;
        private readonly IConnectivity connectivity;
        public GuestListOverviewViewModel(OrderService orderService, IConnectivity connectivity)
        {
            this.orderService = orderService;
            this.connectivity = connectivity;
        }

        public async void Init()
        {
            Title = "Guestlist Orders";
            skip = 0;
            OrdersCount = OrderList.Count;
            LoadMore = false;
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            await GetGuestListOrdersAsync();
        }

        [ICommand]
        async Task GetGuestListOrdersAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                CanLoadMore = false;

                if (OrdersCount > 0 && !LoadMore)
                {
                    OrderList.Clear();
                }

                var response = await orderService.GetGuestListOrders(EventUuid, skip, take);
                if (response.Headers.TryGetValues("X-TF-PAGINATION-TOTAL", out IEnumerable<string> headerValues))
                {
                    TotalItems = Int32.Parse(headerValues.FirstOrDefault());
                }

                var json = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json);

                var orderList = new List<Order>();
                foreach (var order in orders)
                {
                    var fullName = $"{order.CustomerData.FirstName} {order.CustomerData.LastName}";
                    order.FullName = fullName.Length > 15 ? $"{fullName.Substring(0, 15)}..." : fullName;
                    order.TotalBalanceInclVat = FormatHelper.FormatPrice(order.TotalBalanceInclVat);
                    order.Completed = FormatHelper.FormatISO8601ToDateTime(order.CompletedAt);
                    order.CompletedAt = FormatHelper.FormatISO8601ToDateOnlyString(order.CompletedAt);

                    var products = 0;
                    foreach (var ticket in order.TicketTypes)
                    {
                        products += ticket.Amount;
                    }
                    order.Products = products;

                    var formattedOrder = OrderHelper.CalculateOrderStatus(order);
                    orderList.Add(formattedOrder);                    
                }

                var sortedList = orderList.OrderByDescending(order => order.Completed);
                foreach (var order in sortedList)
                {
                    OrderList.Add(order);
                }

                OrdersCount = OrderList.Count;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get orders: {e}");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                LoadMore = false;

                if (TotalItems > OrdersCount)
                {
                    CanLoadMore = true;
                }
            }
        }

        [ICommand]
        async Task RefreshAsync()
        {
            skip = 0;
            await GetGuestListOrdersAsync();
        }

        [ICommand]
        async Task LoadMoreAsync()
        {
            if (TotalItems <= OrdersCount)
                return;

            skip = OrdersCount;
            LoadMore = true;
            await GetGuestListOrdersAsync();
        }

        [ICommand]
        async Task GoToOrderDetails(Order selectedOrder)
        {
            if (selectedOrder is null)
                return;

            await Shell.Current.GoToAsync($"/{nameof(TabBar)}/{nameof(OrderDetailsPage)}", true, new Dictionary<string, object>
            {
                {"SelectedOrder", selectedOrder }
            });
        }

        [ICommand]
        async Task InviteGuestAsync()
        {
            await Shell.Current.GoToAsync($"/{nameof(TabBar)}/{nameof(GuestListInvitationPage)}");
        }
    }
}
