using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Newtonsoft.Json;
using OrganiserApp.Enums;
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

namespace OrganiserApp.ViewModels
{

    public partial class EventOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<Event> EventList { get; set; } = new();

        private readonly EventService eventService;
        private readonly IConnectivity connectivity;

        [ObservableProperty]
        public int eventsCount;
        [ObservableProperty]
        public int totalItems;
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        bool canLoadMore;

        bool LoadMore = false;
        bool FilterChanged = false;
        int skip;
        int take = 5;
        FilterDateRangeType dateRange;
        public List<string> EventListTypes { get; } = Enum.GetNames(typeof(EventListType)).ToList();
        EventListType selectedType = EventListType.Upcoming;
        public EventListType SelectedType
        {
            get => selectedType;
            set => SetProperty(ref selectedType, value);
        }

        public EventOverviewViewModel(EventService eventService, IConnectivity connectivity)
        {
            Title = "Event Overview";
            this.eventService = eventService;
            this.connectivity = connectivity;
            dateRange = FilterDateRangeType.all;

            Task.Run(async () => await GetEventListAsync());
        }

        [ICommand]
        async Task GetEventListAsync()
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
                CanLoadMore = false;
                take = 5;

                if (EventList.Count != 0)
                {
                    if (FilterChanged)
                    {
                        take = EventList.Count;
                        skip = 0;
                    }

                    if (!LoadMore)
                    {
                        EventList.Clear();
                    }                    
                }
                var response = await eventService.GetEventListAsync(selectedType, skip, take);
                if (response.Headers.TryGetValues("X-TF-PAGINATION-TOTAL", out IEnumerable<string> headerValues))
                {
                    TotalItems = Int32.Parse(headerValues.FirstOrDefault());
                }

                var json = await response.Content.ReadAsStringAsync();
                var eventList = JsonConvert.DeserializeObject<IEnumerable<Event>>(json);

                foreach (var item in eventList)
                {
                    item.EventSummary = await eventService.GetEventSummaryAsync(dateRange, item.Uuid);

                    if (!item.IsOngoing)
                    {
                        item.StartAt = FormatHelper.FormatISO8601ToDateTimeString(item.StartAt);
                        item.EndAt = FormatHelper.FormatISO8601ToDateTimeString(item.EndAt);
                        item.EventSummary.Content.Revenue = FormatHelper.FormatPrice(item.EventSummary.Content.Revenue);
                    }

                    EventList.Add(item);
                }

                EventsCount = EventList.Count;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get events: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                LoadMore = false;
                FilterChanged = false;

                if (!(EventList.Count >= totalItems))
                {
                    CanLoadMore = true;
                }

            }
        }

        [ICommand]
        async Task LoadMoreEvents()
        {
            if (IsBusy)
                return;

            LoadMore = true;
            skip += 5;

            await GetEventListAsync();
        }

        [ICommand]
        async Task FilterAll()
        {
            FilterChanged = true;
            dateRange = FilterDateRangeType.all;
            await GetEventListAsync();
        }

        [ICommand]
        async Task FilterYesterday()
        {
            FilterChanged = true;
            dateRange = FilterDateRangeType.yesterday;
            await GetEventListAsync();
        }

        [ICommand]
        async Task FilterToday()
        {
            FilterChanged = true;
            dateRange = FilterDateRangeType.today;
            await GetEventListAsync();
        }
    }
}
