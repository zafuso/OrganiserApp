using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Helpers;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    public partial class GuestListInvitationViewModel : BaseViewModel
    {
        public ObservableCollection<Language> AvailableLanguages { get; set; } = new();

        private readonly TicketService ticketService;
        private readonly CountryService countryService;
        private readonly IConnectivity connectivity;
        private readonly OrderService orderService;

        string EventUuid;

        [ObservableProperty]
        Language selectedLanguage;

        [ObservableProperty]
        GuestListInvitation invitation;

        public GuestListInvitationViewModel(IConnectivity connectivity, TicketService ticketService, 
            CountryService countryService, OrderService orderService)
        {
            this.connectivity = connectivity;
            this.ticketService = ticketService;
            this.countryService = countryService;
            this.orderService = orderService;
        }

        public async void Init()
        {
            Title = "Send Invitation"; 
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            Invitation = new GuestListInvitation
            {
                CustomerData = new(),
                TicketTypes = new()
            };

            await GetTicketTypesAsync();
            await GetAvailableLanguagesAsync();
        }

        [ICommand]
        async Task GetTicketTypesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                if (Invitation.TicketTypes.Count > 0)
                {
                    Invitation.TicketTypes.Clear();
                }

                var ticketTypes = await ticketService.GetTicketTypesAsync(EventUuid);

                foreach (var ticket in ticketTypes)
                {
                    // only add tickets that are enabled for guestlist
                    if (ticket.IsSelectableOnGuestList)
                    {
                        var invitationTicket = new InvitationTicketType
                        {
                            Uuid = ticket.Uuid,
                            Amount = 0,
                            Name = ticket.Name.Nl,
                            Subtitle = ticket.Subtitle.Nl,
                            Price = FormatHelper.FormatPrice(ticket.Price)
                        };

                        Invitation.TicketTypes.Add(invitationTicket);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get tickets: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task GetAvailableLanguagesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (AvailableLanguages.Count > 0)
                {
                    AvailableLanguages.Clear();
                }

                var languages = await countryService.GetLanguagesAsync();

                foreach (var language in languages)
                {
                    AvailableLanguages.Add(language);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to retrieve languages: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task SendGuestListInvitationAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                // Do not send invitation for tickettypes with amount 0
                Invitation.TicketTypes.RemoveAll(ticket => ticket.Amount == 0);

                await orderService.SendGuestListInvitationAsync(EventUuid, Invitation, SelectedLanguage);
                await Shell.Current.GoToAsync($"/{nameof(TabBar)}/{nameof(GuestListOverviewPage)}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to send out invitation: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
