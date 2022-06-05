using Newtonsoft.Json;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class AnalyticsService : BaseHttpClient
    {
        static readonly HttpClient client = GetHttpClient();

        public async Task<TicketTypesAnalytics> GetTicketTypeSalesAnalytics(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/analytics/promoter/sales/tickettypes");
            var analytics = JsonConvert.DeserializeObject<TicketTypesAnalytics>(json);

            return analytics;
        }

        public async Task<GenderAnalytics> GetGenderAnalytics(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/analytics/promoter/sales/genders");
            var analytics = JsonConvert.DeserializeObject<GenderAnalytics>(json);

            return analytics;
        }


        public async Task<AgesAnalytics> GetAgesAnalytics(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/analytics/promoter/sales/ages");
            var analytics = JsonConvert.DeserializeObject<AgesAnalytics>(json);

            return analytics;
        }

        public async Task<CitiesAnalytics> GetCitiesAnalytics(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/analytics/promoter/sales/cities");
            var analytics = JsonConvert.DeserializeObject<CitiesAnalytics>(json);

            return analytics;
        }
    }
}
