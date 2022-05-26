using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Models;
using OrganiserApp.Services;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedEvent", "SelectedEvent")]
    public partial class EventSettingsViewModel : BaseViewModel
    {

        [ObservableProperty]
        Event selectedEvent;

        public EventSettingsViewModel()
        {       
            
        }
    }
}
