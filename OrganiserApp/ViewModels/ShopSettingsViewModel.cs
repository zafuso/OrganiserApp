using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using System;
using System.Collections.Generic;
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
        string EventUuid;

        private readonly ShopService shopService;
        public ShopSettingsViewModel(ShopService shopService)
        {
            this.shopService = shopService;
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

                var isConfirmed = await Shell.Current.DisplayAlert("Demo Ticketshop", $"Your ticketshop url is: {uri}. As this application is not running on production we'll send you to a demo shop instead.", "Okay! Go to shop", "No, take me back.");

                if (!isConfirmed)
                    return;

                IsBusy = true;

                Uri demoUri = new($"{Config.BaseUrlTicketShop}/cmcomtestfestival");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
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
    }
}
