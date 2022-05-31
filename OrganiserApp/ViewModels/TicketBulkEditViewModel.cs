using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using OrganiserApp.Models;
using OrganiserApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("CheckedTicketsList", "CheckedTicketsList")]
    public partial class TicketBulkEditViewModel : BaseViewModel
    {
        public ObservableCollection<TicketType> TicketTypeList { get; set; } = new();

        [ObservableProperty]
        List<TicketStatusType> ticketStatusTypeList;
        [ObservableProperty]
        int ticketCount;
        [ObservableProperty]
        string ticketNames;
        [ObservableProperty]
        bool sliderStatus = false;
        [ObservableProperty]
        bool sliderSalesPeriod = false;
        [ObservableProperty]
        bool sliderValidityPeriod = false;
        [ObservableProperty]
        bool sliderPersonalization = false;

        private readonly TicketService ticketService;
        public TicketBulkEditViewModel(TicketService ticketService)
        {
            Title = "Bulk edit tickets";
            this.ticketService = ticketService;
        }

        public async void Init()
        {
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
                TicketNames += $"{TicketTypeList[i].Name.Nl}, ";

                if (i == (TicketCount - 1))
                {
                    TicketNames += $"{TicketTypeList[i].Name.Nl}";
                }
            }

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
    }
}
