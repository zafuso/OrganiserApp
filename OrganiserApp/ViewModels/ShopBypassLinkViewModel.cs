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
    public partial class ShopBypassLinkViewModel : BaseViewModel
    {
        [ObservableProperty]
        Viewport viewport;

        [ObservableProperty]
        string bypassUrl;

        ViewportBypassUrl BypassObject;
        string EventUuid;

        private readonly ShopService shopService;
        private readonly IClipboard clipboard;
        public ShopBypassLinkViewModel(ShopService shopService, IClipboard clipboard)
        {
            this.shopService = shopService;
            this.clipboard = clipboard;
        }

        public async void Init()
        {
            Title = "Bypass Link";
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            await PostGenerateBypassUrlAsync();
        }

        [ICommand]
        async Task PostGenerateBypassUrlAsync()
        {
            if (IsBusy || Viewport is null)
                return;

            try
            {
                IsBusy = true;
                BypassObject = await shopService.PostGenerateBypassUrlAsync(Viewport, EventUuid);
                BypassUrl = $"{Config.BaseUrlTicketShop}{BypassObject.Url}";
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get bypass url: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task CopyBypassUrlAsync()
        {
            var hasError = false;

            if (BypassObject is null)
                return;

            try
            {
                await Clipboard.Default.SetTextAsync(BypassObject.Url);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Could not copy to clipbard: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
                hasError = true;
            }

            if (!hasError)
                await Shell.Current.DisplayAlert("Link copied", "Shoplink has been copied to clipboard.", "OK");
        }

        [ICommand]
        async Task GoBackToShopAsync()
        {
            if (IsBusy || Viewport is null)
                return;

            try
            {
                IsBusy = true;
                await Shell.Current.GoToAsync($"/{nameof(ShopSettingsPage)}", true, new Dictionary<string, object>
                {
                    {"Viewport", Viewport }
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Could not copy to clipbard: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
