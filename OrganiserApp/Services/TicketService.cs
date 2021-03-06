using Newtonsoft.Json;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class TicketService : BaseHttpClient
    {
        static readonly HttpClient client = GetHttpClient();

        public async Task<IEnumerable<TicketStatusType>> GetTicketstatusTypesAsync()
        {
            var json = await client.GetStringAsync("ticketstatustypes");

            Preferences.Remove("TicketStatusTypesJson");
            Preferences.Set("TicketStatusTypesJson", json);

            var ticketStatusTypes = JsonConvert.DeserializeObject<IEnumerable<TicketStatusType>>(json);

            return ticketStatusTypes;
        }

        public async Task<IEnumerable<TicketType>> GetTicketTypesAsync(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/tickettypes");
            var ticketTypeList = JsonConvert.DeserializeObject<IEnumerable<TicketType>>(json);

            return ticketTypeList;
        }

        public async Task<IEnumerable<TicketCategory>> GetTicketCategoriesAsync(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/ticketcategories");
            var ticketCategories = JsonConvert.DeserializeObject<IEnumerable<TicketCategory>>(json);

            return ticketCategories;
        }

        public async Task<IEnumerable<TicketPoule>> GetTicketPoulesAsync(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/ticketpoules");
            var ticketPoules = JsonConvert.DeserializeObject<IEnumerable<TicketPoule>>(json);

            return ticketPoules;
        }

        public async Task<TicketType> PutTicketTypeAsync(TicketType ticketType, string EventUuid, bool AlertOnSuccess = true, bool MultipleTickets = false)
        {
            var json = JsonConvert.SerializeObject(ticketType);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"events/{EventUuid}/tickettypes/{ticketType.Uuid}", content);

            if (!response.IsSuccessStatusCode)
            {
                _ = Application.Current.MainPage.DisplayAlert("Failed", "Failed to update ticket.", "Cancel");
            }
            else
            {
                if (AlertOnSuccess)
                {
                    if (MultipleTickets)
                    {
                        _ = Application.Current.MainPage.DisplayAlert("Tickets saved", "The tickets have been updated.", "OK");
                    }
                    else
                    {
                        _ = Application.Current.MainPage.DisplayAlert("Ticket saved", "The ticket has been updated.", "OK");
                    }
                }
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TicketType>(responseJson);
        }

        public async Task<TicketPoule> PostTicketPouleAsync(TicketPoule ticketPoule, string EventUuid)
        {
            var json = JsonConvert.SerializeObject(ticketPoule);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"events/{EventUuid}/ticketpoules", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TicketPoule>(responseJson);            
        }

        public async Task PutTicketcategoryAsync(TicketCategory category, string EventUuid)
        {
            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"events/{EventUuid}/ticketcategories/{category.Uuid}", content);
            response.EnsureSuccessStatusCode();

            _ = Application.Current.MainPage.DisplayAlert("Ticket category saved", "The category has been updated.", "OK");
        }
    }
}
