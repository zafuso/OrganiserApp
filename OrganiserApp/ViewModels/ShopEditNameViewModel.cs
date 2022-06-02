using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    [QueryProperty("Viewport", "Viewport")]
    public partial class ShopEditNameViewModel: BaseViewModel
    {
        [ObservableProperty]
        Viewport viewport;
        private readonly ShopService shopService;
        string EventUuid;
        public ShopEditNameViewModel(ShopService shopService)
        {
            this.shopService = shopService;
        }

        public async void Init()
        {
            Title = "Edit Shop";
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");
        }

        [ICommand]
        async Task GoBackToShopOverviewAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(ShopOverviewPage)}");
        }

        [ICommand]
        async Task UpdateShopAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await shopService.PutViewportAsync(Viewport, EventUuid);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to update shop: {e}");
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
