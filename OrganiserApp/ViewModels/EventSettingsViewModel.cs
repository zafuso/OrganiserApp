using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Helpers;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Venue;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedEvent", "SelectedEvent")]
    public partial class EventSettingsViewModel : BaseViewModel
    {

        [ObservableProperty]
        Event selectedEvent;

        [ObservableProperty]
        Venue selectedVenue;

        [ObservableProperty]
        bool isValidName;

        [ObservableProperty]
        bool isValidForm;

        [ObservableProperty]
        bool hasDownloadFrom;

        [ObservableProperty]
        bool isNotDownloadable;

        [ObservableProperty]
        TimeSpan downloadFromTime;

        [ObservableProperty]
        DateTime downloadFromDate;

        [ObservableProperty]
        TimeSpan startAtTime;

        [ObservableProperty]
        DateTime startAtDate;

        [ObservableProperty]
        TimeSpan endAtTime;

        [ObservableProperty]
        DateTime endAtDate;

        [ObservableProperty]
        string minDateStartDate;

        [ObservableProperty]
        string minDateEndDate;

        public ObservableCollection<Venue> VenueList { get; set; } = new();

        private readonly VenueService venueService;
        private readonly EventService eventService;
        private readonly IConnectivity connectivity;

        public EventSettingsViewModel(VenueService venueService, EventService eventService, IConnectivity connectivity)
        {
            Title = "Event Settings";
            this.venueService = venueService;
            this.eventService = eventService;
            this.connectivity = connectivity;
        }

        public async void Init()
        {            
            await GetVenuesAsync();
            BindEventProperties();
        }

        void BindEventProperties()
        {
            MinDateStartDate = DateTime.Now.ToString("MM/dd/yyyy");

            // Get correct downloadsettings for radiobuttons
            if (selectedEvent.DownloadFrom != null)
            {
                HasDownloadFrom = true;
                DownloadFromDate = FormatHelper.FormatISO8601ToDate(selectedEvent.DownloadFrom);
                DownloadFromTime = FormatHelper.FormatISO8601ToTime(selectedEvent.DownloadFrom);

                var DownloadFrom = FormatHelper.FormatISO8601ToDateTime(selectedEvent.DownloadFrom);
                if (DownloadFrom > DateTime.Now)
                {
                    selectedEvent.IsDownloadable = false;
                }
            }
            else
            {
                HasDownloadFrom = false;
                DownloadFromDate = DateTime.Now.Date;
                DownloadFromTime = DateTime.Now.TimeOfDay;

                if (!selectedEvent.IsDownloadable)
                {
                    IsNotDownloadable = true;
                }
            }

            // set event start and end date / time.
            if (!selectedEvent.IsOngoing)
            {
                StartAtDate = FormatHelper.FormatISO8601ToDate(selectedEvent.StartAt);
                StartAtTime = FormatHelper.FormatISO8601ToTime(selectedEvent.StartAt);

                EndAtDate = FormatHelper.FormatISO8601ToDate(selectedEvent.EndAt);
                EndAtTime = FormatHelper.FormatISO8601ToTime(selectedEvent.EndAt);

                MinDateEndDate = StartAtDate.ToString("MM/dd/yyyy");
            }
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

        [ICommand]
        async Task SaveEventSettingsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            selectedEvent.VenueUuid = SelectedVenue.Uuid;
            selectedEvent.StartAt = FormatHelper.FormatDateTimeToISO8601String(StartAtDate, startAtTime);
            selectedEvent.EndAt = FormatHelper.FormatDateTimeToISO8601String(EndAtDate, EndAtTime);

            if (HasDownloadFrom)
            {
                selectedEvent.IsDownloadable = true;
                selectedEvent.DownloadFrom = FormatHelper.FormatDateTimeToISO8601String(DownloadFromDate, DownloadFromTime);
            }

            try
            {
                SelectedEvent = await eventService.PutEventAsync(selectedEvent);
                BindEventProperties();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to save event: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task GoToCreateVenueModal()
        {
            await Shell.Current.GoToAsync($"/{nameof(TabBar)}/{nameof(CreateVenuePage)}", true, new Dictionary<string, object>
            {
                {"SelectedEvent", selectedEvent }
            });
        }

        public void ValidateForm()
        {
            IsValidForm = false;

            if (IsValidName)
            {
                IsValidForm = true;
            }
        }
    }
}
