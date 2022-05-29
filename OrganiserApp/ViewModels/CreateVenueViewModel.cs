using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganiserApp.Models;
using OrganiserApp.Services;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;
using OrganiserApp.Views.Event;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedEvent", "SelectedEvent")]
    public partial class CreateVenueViewModel : BaseViewModel
    {
        [ObservableProperty]
        Event selectedEvent;

        [ObservableProperty]
        Country selectedCountry;

        [ObservableProperty]
        Venue newVenue = new();

        public ObservableCollection<Country> CountryList { get; set; } = new();

        private readonly VenueService venueService;
        private readonly CountryService countryService;
        private readonly IConnectivity connectivity;
        private readonly IGeolocation geolocation;

        public CreateVenueViewModel(VenueService venueService, IConnectivity connectivity, 
            CountryService countryService, IGeolocation geolocation)
        {
            Title = "Create Location";
            this.venueService = venueService;
            this.countryService = countryService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }

        public async void Init()
        {
            await GetCountriesAsync();
            await GetUserLocation();
        }

        [ICommand]
        async Task GetCountriesAsync()
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
                var countryList = await countryService.GetCountriesAsync();

                foreach (var country in countryList)
                {
                    CountryList.Add(country);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to load page: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task CreateVenueAsync()
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

                NewVenue.CountryId = SelectedCountry.Id;

                var createdVenue = await venueService.PostVenueAsync(NewVenue);
                selectedEvent.VenueUuid = createdVenue.Uuid;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to create location: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventSettingsPage)}", true, new Dictionary<string, object>
                {
                    {"SelectedEvent", selectedEvent }
                });
            }
        }

        [ICommand]
        async Task GetUserLocation()
        {
            try
            {
                // Get cached location, else get real location.
                var location = await geolocation.GetLastKnownLocationAsync();
                if (location is null)
                {
                    location = await geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location is null)
                    return;

                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
                Placemark placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    SelectedCountry = CountryList.Where(c => c.Id == placemark.CountryCode).FirstOrDefault();
                    NewVenue.AddressLine1 = $"{placemark.SubThoroughfare} {placemark.Thoroughfare}";
                    NewVenue.City = placemark.Locality;
                    NewVenue.Zipcode = placemark.PostalCode;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to query location: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }
    }
}
