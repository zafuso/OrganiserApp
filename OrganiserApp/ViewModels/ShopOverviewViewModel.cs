using CommunityToolkit.Mvvm.ComponentModel;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganiserApp.Services;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using OrganiserApp.Views.Shop;
using OrganiserApp.Views.Event;

namespace OrganiserApp.ViewModels
{
    public partial class ShopOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<Viewport> ViewportList { get; set; } = new();

        [ObservableProperty]
        bool isRefreshing;
        string EventUuid;

        private readonly ShopService shopService;

        public ShopOverviewViewModel(ShopService shopService)
        {
            Title = "Shops";

            this.shopService = shopService;
        }

        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            await GetViewportsAsync();
        }

        [ICommand]
        async Task GetViewportsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (ViewportList.Count > 0)
                {
                    ViewportList.Clear();
                }

                var viewports = await shopService.GetViewportsAsync(EventUuid);

                foreach (var viewport in viewports)
                {
                    ViewportList.Add(viewport);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get categories: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                isRefreshing = false;
            }
        }

        [ICommand]
        async Task GoToCreateShopAsync()
        {
            if (IsBusy)
                return;

            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(CreateShopPage)}");
        }

        [ICommand]
        async Task GoToShopSettingsAsync(Viewport viewport)
        {
            if (viewport is null)
                return;

            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(ShopSettingsPage)}", true, new Dictionary<string, object>
            {
                {"Viewport", viewport }
            });
        }
    }
}
