using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Models;
using OrganiserApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganiserApp.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Newtonsoft.Json;
using OrganiserApp.Views.Event;
using OrganiserApp.Helpers;
using OrganiserApp.Views.Ticket;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedTicket", "SelectedTicket")]
    public partial class TicketDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<Models.TicketStatusType> TicketStatusTypeList { get; set; } = new();
        public ObservableCollection<TicketPoule> TicketPouleList { get; set; } = new();
        string EventUuid;

        [ObservableProperty]
        TicketType selectedTicket;

        [ObservableProperty]
        Models.TicketStatusType selectedStatus;

        [ObservableProperty]
        TicketPoule selectedPoule;

        [ObservableProperty]
        bool newPouleSelected;

        [ObservableProperty]
        bool existingPouleSelected;

        [ObservableProperty]
        int capacity;

        [ObservableProperty]
        bool switchSalesPeriod;

        [ObservableProperty]
        bool switchValidityPeriod;

        [ObservableProperty]
        TimeSpan onlineFromTime;

        [ObservableProperty]
        DateTime onlineFromDate;

        [ObservableProperty]
        TimeSpan onlineUntilTime;

        [ObservableProperty]
        DateTime onlineUntilDate;

        [ObservableProperty]
        TimeSpan validFromTime;

        [ObservableProperty]
        DateTime validFromDate;

        [ObservableProperty]
        TimeSpan validUntilTime;

        [ObservableProperty]
        DateTime validUntilDate;

        private readonly TicketService ticketService;

        public TicketDetailsViewModel(TicketService ticketService)
        {
            Title = "Ticket Settings";

            this.ticketService = ticketService;
        }

        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");
           
            BindTicketProperties();

            var TicketStatusTypesJson = Preferences.Get("TicketStatusTypesJson", null);
            await GetTicketStatusTypesAsync(TicketStatusTypesJson);
            await GetTicketPoulesAsync();
        }

        [ICommand]
        async Task GetTicketStatusTypesAsync(string TicketStatusTypesJson = null)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (TicketStatusTypeList.Count != 0)
                {
                    TicketStatusTypeList.Clear();
                }

                IEnumerable<Models.TicketStatusType> ticketStatusTypes;

                if (TicketStatusTypesJson is null)
                {
                    ticketStatusTypes = await ticketService.GetTicketstatusTypesAsync();
                } 
                else
                {
                    ticketStatusTypes = JsonConvert.DeserializeObject<IEnumerable<Models.TicketStatusType>>(TicketStatusTypesJson);
                }                

                foreach (var status in ticketStatusTypes)
                {
                    TicketStatusTypeList.Add(status);
                }

                SelectedStatus = TicketStatusTypeList.Where(status => status.Id == SelectedTicket.TicketStatusType.ToString()).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get ticket status types: {e}");
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

                SelectedPoule = TicketPouleList.Where(poule => poule.Uuid == SelectedTicket.TicketPouleUuid).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get poules: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        Task ExistingPouleClicked()
        {
            NewPouleSelected = false;
            ExistingPouleSelected = true;
            return Task.CompletedTask;
        }

        [ICommand]
        Task NewPouleClicked()
        {
            NewPouleSelected = true;
            ExistingPouleSelected = false;
            return Task.CompletedTask;
        }

        void BindTicketProperties()
        {
            NewPouleSelected = false;
            ExistingPouleSelected = true;

            if (SelectedTicket.OnlineFrom is null)
            {
                SwitchSalesPeriod = false;
            }
            else
            {
                SwitchSalesPeriod = true;

                OnlineFromDate = FormatHelper.FormatISO8601ToDate(SelectedTicket.OnlineFrom);
                OnlineFromTime = FormatHelper.FormatISO8601ToTime(SelectedTicket.OnlineFrom);

                OnlineUntilDate = FormatHelper.FormatISO8601ToDate(SelectedTicket.OnlineUntil);
                OnlineUntilTime = FormatHelper.FormatISO8601ToTime(SelectedTicket.OnlineUntil);
            }

            if (SelectedTicket.StartAt is null)
            {
                SwitchValidityPeriod = false;
            }
            else
            {
                SwitchValidityPeriod = true;

                ValidFromDate = FormatHelper.FormatISO8601ToDate(SelectedTicket.StartAt);
                ValidFromTime = FormatHelper.FormatISO8601ToTime(SelectedTicket.StartAt);

                ValidUntilDate = FormatHelper.FormatISO8601ToDate(SelectedTicket.EndAt);
                ValidUntilTime = FormatHelper.FormatISO8601ToTime(SelectedTicket.EndAt);
            }
        }

        [ICommand]
        async Task SaveTicketTypeAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                SelectedTicket.Price = FormatHelper.FormatPrice(SelectedTicket.Price);

                switch (SelectedStatus.Id)
                {
                    case "ONLINE":
                        SelectedTicket.TicketStatusType = Enums.TicketStatusType.ONLINE;
                        break;
                    case "NOT_IN_SALE":
                        SelectedTicket.TicketStatusType = Enums.TicketStatusType.NOT_IN_SALE;
                        break;
                    case "DOOR_SALE":
                        SelectedTicket.TicketStatusType = Enums.TicketStatusType.DOOR_SALE;
                        break;
                    case "IN_RESERVATION":
                        SelectedTicket.TicketStatusType = Enums.TicketStatusType.IN_RESERVATION;
                        break;
                    case "SOLD_OUT":
                        SelectedTicket.TicketStatusType = Enums.TicketStatusType.SOLD_OUT;
                        break;
                    default: break;
                }

                if (SwitchSalesPeriod)
                    {
                        SelectedTicket.OnlineFrom = FormatHelper.FormatDateTimeToISO8601String(OnlineFromDate, OnlineFromTime);
                        SelectedTicket.OnlineUntil = FormatHelper.FormatDateTimeToISO8601String(OnlineUntilDate, OnlineUntilTime);
                    }

                if (SwitchValidityPeriod)
                {
                        SelectedTicket.StartAt = FormatHelper.FormatDateTimeToISO8601String(ValidFromDate, ValidFromTime);
                        SelectedTicket.EndAt = FormatHelper.FormatDateTimeToISO8601String(ValidUntilDate, ValidUntilTime);
                }

                if (NewPouleSelected)
                {
                    var ticketPoule = new TicketPoule
                    {
                        Capacity = Capacity,
                        IsUnlimited = false,
                        Name = SelectedTicket.Name.Nl
                    };

                    var response = await ticketService.PostTicketPouleAsync(ticketPoule, EventUuid);
                    SelectedTicket.TicketPouleUuid = response.Uuid;
                }

                SelectedTicket = await ticketService.PutTicketTypeAsync(SelectedTicket, EventUuid, true, false);
                BindTicketProperties();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to update ticket: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(TicketOverviewPage)}");
            }
        }
    }
}
