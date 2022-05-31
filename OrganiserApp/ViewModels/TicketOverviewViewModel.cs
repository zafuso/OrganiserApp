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

namespace OrganiserApp.ViewModels
{
    public partial class TicketOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<TicketType> TicketList { get; set; } = new();
        public ObservableCollection<TicketPoule> TicketPouleList { get; set; } = new();
        public ObservableCollection<TicketCategory> TicketCategoryList { get; set; } = new();
        public ObservableCollection<Grouping<string, TicketType>> TicketGroups { get; set; } = new();

        [ObservableProperty]
        string searchQuery;
        string EventUuid;

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
            SearchQuery = EventUuid;

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

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

                var ticketTypes = await ticketService.GetTicketTypesAsync(EventUuid);

                foreach (var ticket in ticketTypes)
                {
                    TicketList.Add(ticket);
                }

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

        void CalculateTicketStatus(TicketType ticket)
        {
            switch (ticket.TicketStatusType)
            {
                case TicketStatusType.ONLINE:
                    ticket.Status = "Online";
                    break;
                case TicketStatusType.NOT_IN_SALE:
                    ticket.Status = "Currently not in sale";
                    break;
                case TicketStatusType.DOOR_SALE:
                    ticket.Status = "Offline";
                    break;
                case TicketStatusType.IN_RESERVATION:
                    ticket.Status = "In reservation";
                    break;
                case TicketStatusType.SOLD_OUT:
                    ticket.Status = "Sold out";
                    break;
                default: break;
            }

            var onlineFrom = FormatHelper.FormatISO8601ToDateTime(ticket.OnlineFrom);
            var onlineUntil = FormatHelper.FormatISO8601ToDateTime(ticket.OnlineUntil);

            if (ticket.TicketStatusType == TicketStatusType.NOT_IN_SALE && onlineFrom > DateTime.Now && onlineUntil < DateTime.Now)
            {
                ticket.Status = "Online";
            }
        }
    }
}
