using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using OrganiserApp.Helpers;
using OrganiserApp.Models;
using OrganiserApp.Enums;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketStatusType = OrganiserApp.Enums.TicketStatusType;
using OrganiserApp.Views.Ticket;
using Newtonsoft.Json;

namespace OrganiserApp.ViewModels
{
    public partial class TicketOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<TicketType> TicketList { get; set; } = new();
        public ObservableCollection<TicketPoule> TicketPouleList { get; set; } = new();
        public ObservableCollection<TicketCategory> TicketCategoryList { get; set; } = new();
        public ObservableCollection<Grouping<string, TicketType>> TicketGroups { get; set; } = new();

        [ObservableProperty]
        bool canBulkEdit = false;
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string searchQuery;
        string EventUuid;
        List<TicketType> ticketTypeList;
        List<TicketType> CheckedTicketsList = new();

        private readonly IConnectivity connectivity;
        private readonly TicketService ticketService;

        public TicketOverviewViewModel(IConnectivity connectivity, TicketService ticketService)
        {
            Title = "Tickets";
            this.connectivity = connectivity;
            this.ticketService = ticketService;
        }

        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            await GetTicketCategoriesAsync();
            await GetTicketPoulesAsync();
            await GetTicketTypesAsync();
        }

        [ICommand]
        async Task RefreshAsync()
        {
            await GetTicketCategoriesAsync();
            await GetTicketPoulesAsync();
            await GetTicketTypesAsync();
        }

        [ICommand]
        async Task GetTicketTypesAsync()
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

                if (TicketList.Count != 0)
                {
                    TicketList.Clear();
                }

                if (CheckedTicketsList.Count != 0)
                {
                    CheckedTicketsList.Clear();
                }

                var ticketTypes = await ticketService.GetTicketTypesAsync(EventUuid);

                foreach (var ticket in ticketTypes)
                {
                    TicketList.Add(ticket);
                    CalculateTicketStatus(ticket);
                }

                ticketTypeList = TicketList.ToList();

                if (TicketCategoryList.Count > 0)
                {
                    foreach (var category in TicketCategoryList)
                    {
                        var groupedTickets = TicketList.Where(t => t.TicketCategoryUuid == category.Uuid).ToList();
                        TicketGroups.Add(new Grouping<string, TicketType>(category.Name.Nl, groupedTickets));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get tickets: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }

        }

        [ICommand]
        async Task GetTicketPoulesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var ticketPoules = await ticketService.GetTicketPoulesAsync(EventUuid);

                foreach (var poule in ticketPoules)
                {
                    TicketPouleList.Add(poule);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get poules: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task GetTicketCategoriesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var ticketCategories = await ticketService.GetTicketCategoriesAsync(EventUuid);

                foreach (var category in ticketCategories)
                {
                    TicketCategoryList.Add(category);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get categories: {e}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        Task SearchTicketTypesAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                SearchQuery = string.Empty;
            }

            SearchQuery = searchQuery.ToLowerInvariant().Trim();
            var filteredItems = ticketTypeList.Where(ticket => ticket.Name.Nl.ToLowerInvariant().Contains(SearchQuery)).ToList();

            foreach (var ticket in ticketTypeList)
            {
                if (!filteredItems.Contains(ticket))
                {
                    TicketList.Remove(ticket);
                }
                else if (!TicketList.Contains(ticket))
                {
                    TicketList.Add(ticket);
                }
            }

            return Task.CompletedTask;
        }

        public void TicketTypeCheckedAsync()
        {
            foreach (var ticket in TicketList)
            {
                if (ticket.IsChecked)
                {
                    bool TicketInList = CheckedTicketsList.Any(t => t.Uuid == ticket.Uuid);

                    if (!TicketInList)
                    {
                        CheckedTicketsList.Add(ticket);
                    }                  
                }

                if (CheckedTicketsList.Count > 1)
                {
                    CanBulkEdit = true;
                }
                else
                {
                    CanBulkEdit = false;
                }
            }
        }

        [ICommand]
        async Task GoToBulkEditAsync()
        {
            if (CheckedTicketsList.Count < 2)
                return;

            Preferences.Remove("TicketList");

            var json = JsonConvert.SerializeObject(CheckedTicketsList);
            Preferences.Set("TicketList", json);

            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(TicketBulkEditPage)}");
        }

        void CalculateTicketStatus(TicketType ticket)
        {
            switch (ticket.TicketStatusType)
            {
                case TicketStatusType.ONLINE:
                    ticket.Status = "Online";
                    ticket.StatusIndicator = "status_success.png";
                    break;
                case TicketStatusType.NOT_IN_SALE:
                    ticket.Status = "Currently not in sale";
                    ticket.StatusIndicator = "status_error.png";
                    break;
                case TicketStatusType.DOOR_SALE:
                    ticket.Status = "Offline";
                    ticket.StatusIndicator = "status_error.png";
                    break;
                case TicketStatusType.IN_RESERVATION:
                    ticket.Status = "In reservation";
                    ticket.StatusIndicator = "status_warning.png";
                    break;
                case TicketStatusType.SOLD_OUT:
                    ticket.Status = "Sold out";
                    ticket.StatusIndicator = "status_error.png";
                    break;
                default: break;
            }

            if (ticket.OnlineFrom != null && ticket.OnlineUntil != null) {
                
                var onlineFrom = FormatHelper.FormatISO8601ToDateTime(ticket.OnlineFrom);
                var onlineUntil = FormatHelper.FormatISO8601ToDateTime(ticket.OnlineUntil);

                if (ticket.TicketStatusType == TicketStatusType.NOT_IN_SALE && onlineFrom < DateTime.Now && onlineUntil > DateTime.Now)
                {
                    ticket.Status = $"Online until {FormatHelper.FormatISO8601ToDateString(ticket.OnlineUntil)}";
                    ticket.StatusIndicator = "status_success.png";
                }
            }
        }
    }
}
