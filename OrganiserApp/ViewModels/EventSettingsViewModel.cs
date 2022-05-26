using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using OrganiserApp.Models;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("EventUuid", "EventUuid")]
    public partial class EventSettingsViewModel : BaseViewModel
    {
        [ObservableProperty]
        string eventUuid;

        public EventSettingsViewModel()
        {

        }

    }
}
