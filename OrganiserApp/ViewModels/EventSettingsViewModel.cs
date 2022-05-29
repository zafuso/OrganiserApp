using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [QueryProperty("SelectedEvent", "SelectedEvent")]
    public partial class EventSettingsViewModel : BaseViewModel
    {

        [ObservableProperty]
        Event selectedEvent;

        [ObservableProperty]
        Venue selectedVenue;

        public ObservableCollection<Venue> VenueList { get; set; } = new();

        private readonly VenueService venueService;
        private readonly IConnectivity connectivity;

        public EventSettingsViewModel(VenueService venueService, IConnectivity connectivity)
        {
            Title = "Event Settings";
            this.venueService = venueService;
            this.connectivity = connectivity;
        }

        public async void Init()
        {
            await GetVenuesAsync();
        }

        [ICommand]
        async Task GetVenuesAsync()
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

                var venues = await venueService.GetVenuesAsync();

                foreach (var venue in venues)
                {
                    VenueList.Add(venue);
                }

                SelectedVenue = VenueList.Where(venue => venue.Uuid == selectedEvent.VenueUuid).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get venues: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
