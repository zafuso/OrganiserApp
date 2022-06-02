using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Helpers;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Shop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("Viewport", "Viewport")]
    public partial class ShopSettingsViewModel : BaseViewModel
    {
        public ObservableCollection<TicketType> TicketList { get; set; } = new();
        public ObservableCollection<TicketType> AvailableTickets { get; set; } = new();
        public ObservableCollection<TicketType> UnAvailableTickets { get; set; } = new();

        [ObservableProperty]
        Viewport viewport;
        string EventUuid;

        private readonly ShopService shopService;
        private readonly TicketService ticketService;
        private readonly IConnectivity connectivity;
        public ShopSettingsViewModel(ShopService shopService, TicketService ticketService, IConnectivity connectivity)
        {
            this.shopService = shopService;
            this.ticketService = ticketService;
            this.connectivity = connectivity;
        }

        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            Title = Viewport.Uri;

        }

        [ICommand]
        async Task GoToTicketShopAsync()
        {
            if (IsBusy || Viewport.Uri is null)
                return;

            try
            {
                Uri uri = new($"{Config.BaseUrlTicketShop}/{Viewport.Uri}");

                var isConfirmed = await Shell.Current.DisplayAlert("Demo Ticketshop", $"Your ticketshop url is: {uri}. As this application is not running on production we'll send you to a demo shop instead.", "Okay! Go to shop", "Cancel");

                if (!isConfirmed)
                    return;

                IsBusy = true;

                Uri demoUri = new($"{Config.BaseUrlTicketShop}/cmcomtestfestival");
                await Browser.Default.OpenAsync(demoUri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to open browser: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task RemoveViewportAsync()
        {
            if (Viewport is null || IsBusy)
                return;

            try
            {
                var isConfirmed = await Shell.Current.DisplayAlert("Delete Event", $"Are you sure you want to delete shop {Viewport.Uri}?", "Delete", "Cancel");

                if (!isConfirmed)
                    return;

                IsBusy = true;
                await shopService.RemoveViewport(Viewport, EventUuid);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to delete shop: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(ShopOverviewPage)}");
        }

        [ICommand]
        async Task GoToBypassLinkPageAsync()
        {
            if (IsBusy || Viewport is null)
                return;

            await Shell.Current.GoToAsync($"/{nameof(ShopBypassLinkPage)}", true, new Dictionary<string, object>
            {
                {"Viewport", Viewport }
            });
        }

        [ICommand]
        async Task GoToEditShopPageAsync()
        {
            if (IsBusy || Viewport is null)
                return;

            await Shell.Current.GoToAsync($"/{nameof(ShopEditNamePage)}", true, new Dictionary<string, object>
            {
                {"Viewport", Viewport }
            });
        }

        [ICommand]
        async Task GetTicketTypesAsync()
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

                if (TicketList.Count > 0)
                {
                    TicketList.Clear();
                }

                var ticketTypes = await ticketService.GetTicketTypesAsync(EventUuid);

                foreach (var ticket in ticketTypes)
                {
                    TicketList.Add(ticket);
                }

                if (TicketList.Count > 0)
                {
                    var viewportTickets = new List<string>();
                    foreach (var ticket in Viewport.TicketTypes)
                    {
                        viewportTickets.Add(ticket.Uuid);
                    }

                    var _availableTickets = TicketList.Where(t => viewportTickets.Contains(t.Uuid));
                    AvailableTickets = new ObservableCollection<TicketType>(_availableTickets);

                    var _unavailableTickets = TicketList.Where(t => !viewportTickets.Contains(t.Uuid));
                    UnAvailableTickets = new ObservableCollection<TicketType>(_unavailableTickets);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get tickets: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
