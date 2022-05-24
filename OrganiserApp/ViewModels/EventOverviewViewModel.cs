using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using OrganiserApp.Enums;
using OrganiserApp.Models;
using OrganiserApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{

    public partial class EventOverviewViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Event> EventList { get; set; }
        EventService eventService;
        IConnectivity connectivity;
        int skip;

        [ObservableProperty]
        bool isRefreshing;

        public EventOverviewViewModel(EventService eventService, IConnectivity connectivity)
        {
            Title = "Event Overview";
            this.eventService = eventService;
            this.connectivity = connectivity;

            skip = 0;
        }

        [ICommand]
        async Task GetEventListAsync(EventListType type)
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
                var eventList = await eventService.GetEventListAsync(type, skip);

                if (EventList.Count != 0)
                    EventList.Clear();

                EventList.AddRange(eventList);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get events: {e.Message}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
