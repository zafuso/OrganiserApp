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
        [ObservableProperty]
        Viewport viewport;
        [ObservableProperty]
        int ticketsInShop;

        public ObservableCollection<ShopStep> ShopStepList = new();

        private ObservableCollection<ShopTicketsViewModel> _items = new();
        public ObservableCollection<ShopTicketsViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private ObservableCollection<ShopTicketsGroupViewModel> _groupedItems = new();
        public ObservableCollection<ShopTicketsGroupViewModel> GroupedItems
        {
            get { return _groupedItems; }
            set { SetProperty(ref _groupedItems, value); }
        }

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
            await GetShopStepsAsync();
            await GetTicketTypesAsync();
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

                if (Items.Count > 0)
                {
                    Items.Clear();
                    GroupedItems.Clear();
                }

                var viewportTicketUuids = new List<string>();
                foreach (var shopStep in ShopStepList)
                {
                    var shopStepTickets = shopStep.TicketTypes;
                    foreach (var uuid in shopStepTickets)
                    {
                        viewportTicketUuids.Add(uuid);
                    }
                }

                var ticketTypes = await ticketService.GetTicketTypesAsync(EventUuid);

                foreach (var ticket in ticketTypes)
                {
                    if (viewportTicketUuids.Contains(ticket.Uuid))
                    {
                        Items.Add(new ShopTicketsViewModel { Category = "Available Tickets", Ticket = ticket });
                    }
                    else
                    {
                        Items.Add(new ShopTicketsViewModel { Category = "Other Tickets", Ticket = ticket });
                    }
                }
                var groupedItems = Items
                    .GroupBy(i => i.Category)
                    .Select(g => new ShopTicketsGroupViewModel(g.Key, g));

                GroupedItems = new ObservableCollection<ShopTicketsGroupViewModel>(groupedItems);
                PrintItemsState();
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

        private void PrintItemsState()
        {
            TicketsInShop = GroupedItems[0].Count();
            Debug.WriteLine($"Items {Items.Count}, state:");
            for (int i = 0; i < Items.Count; i++)
            {
                Debug.WriteLine($"\t{i}: Group: {Items[i].Category} | Item: {Items[i].Ticket.Name.Nl}");
            }
        }

        [ICommand]
        private void OnItemDragged(ShopTicketsViewModel item)
        {
            Debug.WriteLine($"OnItemDragged: {item?.Ticket.Name.Nl}");
            foreach (var i in Items)
            {
                i.IsBeingDragged = item == i;
            }
        }

        [ICommand]
        private void OnItemDraggedOver(ShopTicketsViewModel item)
        {
            Debug.WriteLine($"OnItemDraggedOver: {item?.Ticket.Name.Nl}");
            var itemBeingDragged = _items.FirstOrDefault(i => i.IsBeingDragged);
            foreach (var i in Items)
            {
                i.IsBeingDraggedOver = item == i && item != itemBeingDragged;
            }
        }


        [ICommand]
        private void OnItemDragLeave(ShopTicketsViewModel item)
        {
            Debug.WriteLine($"OnItemDragLeave: {item?.Ticket.Name.Nl}");
            foreach (var i in Items)
            {
                i.IsBeingDraggedOver = false;
            }
        }

        [ICommand]
        private void OnItemDropped(ShopTicketsViewModel item)
        {
            var itemToMove = _items.First(i => i.IsBeingDragged);
            var itemToInsertBefore = item;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;

            var categoryToMoveFrom = GroupedItems.First(g => g.Contains(itemToMove));
            categoryToMoveFrom.Remove(itemToMove);

            var categoryToMoveTo = GroupedItems.First(g => g.Contains(itemToInsertBefore));
            var insertAtIndex = categoryToMoveTo.IndexOf(itemToInsertBefore);
            itemToMove.Category = categoryToMoveFrom.Name;
            categoryToMoveTo.Insert(insertAtIndex, itemToMove);
            itemToMove.IsBeingDragged = false;
            itemToInsertBefore.IsBeingDraggedOver = false;
            Debug.WriteLine($"OnItemDropped: [{itemToMove?.Ticket.Name.Nl}] => [{itemToInsertBefore?.Ticket.Name.Nl}], target index = [{insertAtIndex}]");

            PrintItemsState();
        }

        [ICommand]
        async Task GetShopStepsAsync()
        {
            if (IsBusy || Viewport is null)
                return;

            try
            {
                IsBusy = true;

                if (ShopStepList.Count > 0)
                {
                    ShopStepList.Clear();
                }

                var shopSteps = await shopService.GetShopStepsAsync(EventUuid, Viewport.Uuid);

                foreach (var step in shopSteps)
                {
                    ShopStepList.Add(step);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get shop steps: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task UpdateTicketShopAsync()
        {
            if (IsBusy || Viewport is null || ShopStepList.Count == 0)
                return;

            var ticketShopStep = ShopStepList.First();
            if (ticketShopStep is null)
                return;

            try
            {
                var ticketTypeUuidList = new List<string>();

                foreach (var shopTicket in GroupedItems[0])
                {
                    ticketTypeUuidList.Add(shopTicket.Ticket.Uuid);
                }
                ticketShopStep.TicketTypes = ticketTypeUuidList;


                await shopService.PutShopStepAsync(ticketShopStep, EventUuid, Viewport.Uuid);
            } 
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to update shop steps: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                Init();
            }
        }
    }
}
