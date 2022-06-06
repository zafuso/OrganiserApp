﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using OrganiserApp.Enums;
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
    public partial class OrderOverviewViewModel : BaseViewModel
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
        public OrderOverviewViewModel(OrderService orderService, IConnectivity connectivity)
        {
            this.orderService = orderService;
            this.connectivity = connectivity;
        }

        public async void Init()
        {
            Title = "Orders";
            skip = 0;
            OrdersCount = OrderList.Count;
            LoadMore = false;
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            await GetOrdersAsync();
        }

        [ICommand]
        async Task GetOrdersAsync()
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

                var response = await orderService.GetOrders(EventUuid, skip, take);
                if (response.Headers.TryGetValues("X-TF-PAGINATION-TOTAL", out IEnumerable<string> headerValues))
                {
                    TotalItems = Int32.Parse(headerValues.FirstOrDefault());
                }

                var json = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json);

                foreach (var order in orders)
                {
                    var fullName = $"{order.CustomerData.FirstName} {order.CustomerData.LastName}";
                    order.FullName = fullName.Length > 15 ? $"{fullName.Substring(0, 15)}..." : fullName;
                    order.TotalBalanceInclVat = FormatHelper.FormatPrice(order.TotalBalanceInclVat);
                    order.CompletedAt = FormatHelper.FormatISO8601ToDateOnlyString(order.CompletedAt);

                    var products = 0;
                    foreach (var ticket in order.TicketTypes)
                    {
                        products += ticket.Amount;
                    }
                    order.Products = products;

                    CalculateOrderStatus(order);
                    OrderList.Add(order);
                }

                OrdersCount = OrderList.Count;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get orders: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
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
            await GetOrdersAsync();
        }

        [ICommand]
        async Task LoadMoreAsync()
        {
            if (TotalItems <= OrdersCount)
                return;

            skip = OrdersCount;
            LoadMore = true;
            await GetOrdersAsync();
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
        
        void CalculateOrderStatus(Order order)
        {
            switch (order.Status)
            {
                case OrderStatusType.COMPLETED:
                    order.OrderStatus = "Completed";
                    order.StatusIcon = "check_green.svg";
                    order.StatusTextColor = Color.FromArgb("#3DDC97");
                    break;
                case OrderStatusType.REFUNDED:
                    order.OrderStatus = "Refunded";
                    order.StatusIcon = "error_open_red.svg";
                    order.StatusTextColor = Color.FromArgb("#FF495C");
                    break;
                case OrderStatusType.REFUND_PENDING:
                    order.OrderStatus = "Refund pending";
                    order.StatusIcon = "warning_open_orange.svg";
                    order.StatusTextColor = Color.FromArgb("#FFA400");
                    break;
                case OrderStatusType.PARTIALLY_REFUNDED:
                    order.OrderStatus = "Partially Refunded";
                    order.StatusIcon = "warning_open_orange.svg";
                    order.StatusTextColor = Color.FromArgb("#3DDC97");
                    break;
                case OrderStatusType.PARTIALLY_CANCELED:
                    order.OrderStatus = "Partially Cancelled";
                    order.StatusIcon = "warning_open_orange.svg";
                    order.StatusTextColor = Color.FromArgb("#3DDC97");
                    break;
                case OrderStatusType.RELEASED:
                    order.OrderStatus = "Released";
                    order.StatusIcon = "error_open_red.svg";
                    order.StatusTextColor = Color.FromArgb("#FF495C");
                    break;
                case OrderStatusType.CANCELED:
                    order.OrderStatus = "Cancelled";
                    order.StatusIcon = "error_open_red.svg";
                    order.StatusTextColor = Color.FromArgb("#FF495C");
                    break;
                default: break;
            }
        }
    }
}
