using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Ticket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("TicketCategory", "TicketCategory")]
    public partial class TicketCategoryViewModel : BaseViewModel
    {
        string EventUuid;
        [ObservableProperty]
        TicketCategory ticketCategory;

        private readonly TicketService ticketService;
        public TicketCategoryViewModel(TicketService ticketService)
        {
            Title = "Edit Category";
            this.ticketService = ticketService;
        }
        public async void Init()
        {
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");
        }

        [ICommand]
        async Task UpdateTicketCategoryAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await ticketService.PutTicketcategoryAsync(TicketCategory, EventUuid);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to update category: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(TicketOverviewPage)}");
            }
        }
    }
}
