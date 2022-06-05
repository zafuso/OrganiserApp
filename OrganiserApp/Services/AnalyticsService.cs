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
    }
}
