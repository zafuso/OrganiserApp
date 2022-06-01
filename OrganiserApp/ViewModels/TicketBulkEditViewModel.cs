using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using OrganiserApp.Helpers;
using OrganiserApp.Models;
using OrganiserApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganiserApp.Enums;
using OrganiserApp.Views.Event;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("CheckedTicketsList", "CheckedTicketsList")]
    public partial class TicketBulkEditViewModel : BaseViewModel
    {
        public ObservableCollection<TicketType> TicketTypeList { get; set; } = new();
        public ObservableCollection<Models.TicketStatusType> TicketStatusTypeList { get; set; } = new();

        [ObservableProperty]
        int ticketCount;

        [ObservableProperty]
        string ticketNames;

        [ObservableProperty]
        bool switchStatus = false;

        [ObservableProperty]
        bool switchSalesPeriod = false;

        [ObservableProperty]
        bool switchValidityPeriod = false;

        [ObservableProperty]
        bool switchPersonalization = false;

        [ObservableProperty]
        Models.TicketStatusType selectedStatus;

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

        string EventUuid;

        private readonly TicketService ticketService;
        public TicketBulkEditViewModel(TicketService ticketService)
        {
            Title = "Bulk edit tickets";
            this.ticketService = ticketService;
        }

        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            var json = Preferences.Get("TicketList", null);
            var ticketTypeList = JsonConvert.DeserializeObject<IEnumerable<TicketType>>(json);

            foreach (var ticket in ticketTypeList)
            {
                TicketTypeList.Add(ticket);
            }

            TicketCount = TicketTypeList.Count;


            if (TicketCount == 0)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(TicketOverviewViewModel)}");

            for (int i = 0; i < TicketCount; i++)
            {
                if (i == (TicketCount - 1))
                {
                    TicketNames += $"{TicketTypeList[i].Name.Nl}";
                } 
                else
                {
                    TicketNames += $"{TicketTypeList[i].Name.Nl}, ";
                }
            }

            OnlineFromDate = DateTime.Now.Date;
            OnlineUntilDate = DateTime.Now.Date;
            OnlineFromTime = DateTime.Now.Date.TimeOfDay;
            OnlineUntilTime = DateTime.Now.Date.TimeOfDay;

            ValidFromDate = DateTime.Now.Date;
            ValidUntilDate = DateTime.Now.Date;
            ValidFromTime = DateTime.Now.Date.TimeOfDay;
            ValidUntilTime = DateTime.Now.Date.TimeOfDay;

            await GetTicketStatusTypesAsync();
        }

        [ICommand]
        async Task GetTicketStatusTypesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (TicketStatusTypeList.Count != 0)
                {
                    TicketStatusTypeList.Clear();
                }
                var ticketStatusTypes = await ticketService.GetTicketstatusTypesAsync();

                foreach (var status in ticketStatusTypes)
                {
                    TicketStatusTypeList.Add(status);
                }
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
        async Task UpdateTicketTypesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (TicketCount < 2)
                {
                    await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(TicketOverviewViewModel)}");
                }

                var i = 1;
                foreach (var ticket in TicketTypeList)
                {
                    if (SwitchStatus)
                    {
                        switch (SelectedStatus.Id)
                        {
                            case "ONLINE":
                                ticket.TicketStatusType = Enums.TicketStatusType.ONLINE;
                                break;
                            case "NOT_IN_SALE":
                                ticket.TicketStatusType = Enums.TicketStatusType.NOT_IN_SALE;
                                break;
                            case "DOOR_SALE":
                                ticket.TicketStatusType = Enums.TicketStatusType.DOOR_SALE;
                                break;
                            case "IN_RESERVATION":
                                ticket.TicketStatusType = Enums.TicketStatusType.IN_RESERVATION;
                                break;
                            case "SOLD_OUT":
                                ticket.TicketStatusType = Enums.TicketStatusType.SOLD_OUT;
                                break;
                            default: break;
                        }
                    }

                    if (SwitchSalesPeriod)
                    {
                        ticket.OnlineFrom = FormatHelper.FormatDateTimeToISO8601String(OnlineFromDate, OnlineFromTime);
                        ticket.OnlineUntil = FormatHelper.FormatDateTimeToISO8601String(OnlineUntilDate, OnlineUntilTime);
                    }

                    if (SwitchValidityPeriod)
                    {
                        ticket.StartAt = FormatHelper.FormatDateTimeToISO8601String(ValidFromDate, ValidFromTime);
                        ticket.EndAt = FormatHelper.FormatDateTimeToISO8601String(ValidUntilDate, ValidUntilTime);
                    }

                    if (SwitchPersonalization)
                    {
                        ticket.IsPersonalizable = true;
                    }

                    if (i == TicketCount)
                    {
                        await ticketService.PutTicketTypeAsync(ticket, EventUuid, true, true);
                    } 
                    else
                    {
                        await ticketService.PutTicketTypeAsync(ticket, EventUuid, false, true);
                    }

                    i++;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to update ticket types: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(TicketOverviewViewModel)}");
            }
        }
    }
}
