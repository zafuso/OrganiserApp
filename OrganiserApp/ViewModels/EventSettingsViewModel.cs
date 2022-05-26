using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Models;
using OrganiserApp.Services;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("EventUuid", "EventUuid")]
    public partial class EventSettingsViewModel : BaseViewModel
    {
        IConnectivity connectivity;
        EventService eventService;

        [ObservableProperty]
        Event selectedEvent;

        [ObservableProperty]
        string eventUuid;

        public EventSettingsViewModel(IConnectivity connectivity, EventService eventService)
        {
            this.connectivity = connectivity;
            this.eventService = eventService;
           

            Task.Run(async () => await GetEventAsync(eventUuid));
        }

        [ICommand]
        async Task GetEventAsync(string EventUuid)
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


                SelectedEvent = await eventService.GetEventAsync(EventUuid);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get event: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
