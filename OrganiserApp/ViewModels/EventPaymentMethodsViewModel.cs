using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganiserApp.Models;

namespace OrganiserApp.ViewModels
{
    public partial class EventPaymentMethodsViewModel : BaseViewModel
    {
        [ObservableProperty]
        string eventUuid;

        [ObservableProperty]
        Event selectedEvent;

        public EventPaymentMethodsViewModel()
        {
        }
    }
}
