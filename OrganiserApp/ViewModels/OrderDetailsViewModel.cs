using CommunityToolkit.Mvvm.ComponentModel;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedOrder", "SelectedOrder")]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        string EventUuid;

        [ObservableProperty]
        Order selectedOrder;
        public OrderDetailsViewModel()
        {

        }
    }
}
