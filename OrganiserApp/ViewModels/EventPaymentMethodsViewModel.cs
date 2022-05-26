using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("EventUuid", "EventUuid")]
    public partial class EventPaymentMethodsViewModel : BaseViewModel
    {
        [ObservableProperty]
        string eventUuid;

        public EventPaymentMethodsViewModel()
        {
        }
    }
}
