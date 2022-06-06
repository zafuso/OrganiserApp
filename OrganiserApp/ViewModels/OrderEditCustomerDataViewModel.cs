using CommunityToolkit.Mvvm.ComponentModel;
using OrganiserApp.Models;
using OrganiserApp.Views.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedOrder", "SelectedOrder")]
    public partial class OrderEditCustomerDataViewModel : BaseViewModel
    {
        [ObservableProperty]
        Order selectedOrder;
        string EventUuid;

        public OrderEditCustomerDataViewModel()
        {

        }

        public async void Init()
        {
            Title = "Send Invitation";
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

        }
    }
}
