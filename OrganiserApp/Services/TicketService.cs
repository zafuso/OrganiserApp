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
        static readonly HttpClient client = getHttpClient();

        public async Task<IEnumerable<TicketStatusType>> GetTicketstatusTypesAsync()
        {
            var json = await client.GetStringAsync("ticketstatustypes");
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
    }
}
