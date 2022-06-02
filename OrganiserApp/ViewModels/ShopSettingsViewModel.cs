using CommunityToolkit.Mvvm.ComponentModel;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using System;
using System.Collections.Generic;
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
            Title = "Shop";
            this.shopService = shopService;
        }

        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");
        }
    }
}
