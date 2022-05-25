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
        public ObservableRangeCollection<Event> EventList { get; set; }
        public List<VirtualEventFilter> FilterEventsList { get; set; }
        EventService eventService;
        IConnectivity connectivity;

        [ObservableProperty]
        public int eventsCount;
        [ObservableProperty]
        public int totalItems;
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        bool canLoadMore;
        bool LoadMore = false;
        int skip;


        EventListType selectedType = EventListType.Upcoming;
        FilterDateRangeType dateRange;

        VirtualEventFilter selectedFilter = null;
        public VirtualEventFilter SelectedFilter
        {
            get => selectedFilter;
            set => SetProperty(ref selectedFilter, value);
        }

        public EventOverviewViewModel(EventService eventService, IConnectivity connectivity)
        {
            Title = "Event Overview";
            this.eventService = eventService;
            this.connectivity = connectivity;
            EventList = new ObservableRangeCollection<Event>();
            FilterEventsList = new List<VirtualEventFilter>();
           
            PopulateEventListFilter();
            selectedFilter = FilterEventsList[0];
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
                var response = await eventService.GetEventListAsync(selectedType, skip);
                if (response.Headers.TryGetValues("X-TF-PAGINATION-TOTAL", out IEnumerable<string> headerValues)) {
                    totalItems = Int32.Parse(headerValues.FirstOrDefault());
                }

                var json = await response.Content.ReadAsStringAsync();
                var eventList = JsonConvert.DeserializeObject<IEnumerable<Event>>(json);

                if (EventList.Count != 0 && !LoadMore)
                {
                    skip = 0;
                    EventList.Clear();
                }

                foreach (var item in eventList)
                {
                    item.EventSummary = await eventService.GetEventSummaryAsync(dateRange, item.Uuid);

                    if (!item.IsOngoing)
                    {
                        item.StartAt = FormatHelper.FormatISO8601ToDateTimeString(item.StartAt);
                        item.EndAt = FormatHelper.FormatISO8601ToDateTimeString(item.EndAt);
                        item.EventSummary.Content.Revenue = FormatHelper.FormatPrice(item.EventSummary.Content.Revenue);
                    }
                }

                EventList.AddRange(eventList);
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

                if (!(EventList.Count >= totalItems))
                {
                    CanLoadMore = true;
                } 
                else
                {
                    CanLoadMore = false;
                }
            }
        }

        [ICommand]
        async Task LoadMoreEvents()
        {
            LoadMore = true;
            skip += 5;

            await GetEventListAsync();
        }

        [ICommand]
        async Task FilterAll()
        {
            dateRange = FilterDateRangeType.all;
            await GetEventListAsync();
        }

        [ICommand]
        async Task FilterYesterday()
        {
            dateRange = FilterDateRangeType.yesterday;
            await GetEventListAsync();
        }

        [ICommand]
        async Task FilterToday()
        {
            dateRange = FilterDateRangeType.today;
            await GetEventListAsync();
        }

        void PopulateEventListFilter()
        {
            FilterEventsList.Add(new VirtualEventFilter
            {
                Name = "Show upcoming events",
                EventListType = EventListType.Upcoming
            });

            FilterEventsList.Add(new VirtualEventFilter
            {
                Name = "Show past events",
                EventListType = EventListType.Past
            });

            FilterEventsList.Add(new VirtualEventFilter
            {
                Name = "Show deleted events",
                EventListType = EventListType.Deleted
            });

            FilterEventsList.Add(new VirtualEventFilter
            {
                Name = "Show all events",
                EventListType = EventListType.All
            });
        }
    }
}
