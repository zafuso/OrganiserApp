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
        public ObservableCollection<TicketCategory> TicketCategoryList { get; set; } = new();

        public ObservableCollection<TicketGroup> TicketGroups { get; private set; } = new();

        [ObservableProperty]
        bool canBulkEdit = false;
        [ObservableProperty]
        bool isRefreshing;
        string EventUuid;
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
            await GetTicketTypesAsync();
        }

        [ICommand]
        async Task RefreshAsync()
        {
            await GetTicketCategoriesAsync();
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

                if (TicketList.Count > 0)
                {
                    TicketList.Clear();
                }

                if (CheckedTicketsList.Count > 0)
                {
                    CheckedTicketsList.Clear();
                    CanBulkEdit = false;
                }

                if (TicketGroups.Count > 0)
                {
                    TicketGroups.Clear();
                }

                var ticketTypes = await ticketService.GetTicketTypesAsync(EventUuid);

                foreach (var ticket in ticketTypes)
                {
                    ticket.Price = FormatHelper.FormatPrice(ticket.Price);
                    TicketList.Add(ticket);
                    CalculateTicketStatus(ticket);
                }

                if (TicketCategoryList.Count > 0)
                    
                {
                    foreach (var category in TicketCategoryList)
                    {
                        var groupedTickets = TicketList.Where(t => t.TicketCategoryUuid == category.Uuid).ToList();
                        TicketGroups.Add(new TicketGroup(category, groupedTickets));
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
        async Task GetTicketCategoriesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (TicketCategoryList.Count > 0)
                {
                    TicketCategoryList.Clear();
                }

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

        public void TicketTypeCheckedAsync()
        {
            foreach (var ticket in TicketList)
            {

                bool TicketInList = CheckedTicketsList.Any(t => t.Uuid == ticket.Uuid);

                if (ticket.IsChecked && !TicketInList)
                {
                    CheckedTicketsList.Add(ticket);
                }

                if (!ticket.IsChecked && TicketInList)
                {
                    CheckedTicketsList.Remove(ticket);
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

        [ICommand]
        async Task GoToTicketSettingsAsync(TicketType selectedTicket)
        {
            if (selectedTicket is null)
                return;

            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(TicketDetailsPage)}", true, new Dictionary<string, object>
            {
                {"SelectedTicket", selectedTicket }
            });
        }

        [ICommand]
        async Task GoToEditTicketCategoryAsync(TicketCategory ticketCategory)
        {
            if (ticketCategory is null)
                return;

            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(TicketCategoryPage)}", true, new Dictionary<string, object>
            {
                {"TicketCategory", ticketCategory }
            });
        }
    }
}
