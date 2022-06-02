using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Helpers;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Shop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    public partial class CreateShopViewModel : BaseViewModel
    {
        [ObservableProperty]
        Viewport viewport;
        private readonly ShopService shopService;
        string EventUuid;
        public CreateShopViewModel(ShopService shopService)
        {
            Title = "Create new shop";
            this.shopService = shopService;
        }

        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            IsBusy = true;

            Viewport = new Viewport
            {
                IsEnabled = true,
                IsVisible = true,
                SalesTypeId = "OPEN",
                TicketTypes = new List<TicketType>(),
                Uri = FormatHelper.RandomString(16),
            };

            IsBusy = false;
        }

        [ICommand]
        async Task GoBackToShopOverviewAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(ShopOverviewPage)}");
        }

        [ICommand]
        async Task CreateShopAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await shopService.PostViewportAsync(Viewport, EventUuid);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to create shop: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(ShopOverviewPage)}");
            }
        }
    }
}
