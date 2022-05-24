using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using OrganiserApp.Enums;
using OrganiserApp.Helpers;
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
        bool LoadMore = false;

        [ObservableProperty]
        bool isRefreshing;

        EventListType selectedType = EventListType.Upcoming;
        FilterDateRangeType dateRange;
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
            EventList = new ObservableRangeCollection<Event>();

            skip = 0;
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
                var eventList = await eventService.GetEventListAsync(selectedType, skip);

                if (EventList.Count != 0 && LoadMore)
                    EventList.Clear();

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
    }
}
