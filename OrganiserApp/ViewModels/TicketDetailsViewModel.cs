using CommunityToolkit.Mvvm.ComponentModel;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedTicket", "SelectedTicket")]
    public partial class TicketDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        TicketType selectedTicket;
        public TicketDetailsViewModel()
        {
            Title = selectedTicket.Name.Nl; //"Ticket";
        }
    }
}
