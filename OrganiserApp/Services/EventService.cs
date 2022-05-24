using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrganiserApp.Enums;
using OrganiserApp.Models;

namespace OrganiserApp.Services
{
    public class EventService : BaseHttpClient
    {
        public int take = 5;
        static readonly HttpClient client = getHttpClient();

        public async Task<IEnumerable<Event>> GetEventListAsync(EventListType type, int skip)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"events/list?type={type}");
            request.Headers.Add("X-TF-PAGINATION-SKIP", skip.ToString());
            request.Headers.Add("X-TF-PAGINATION-TAKE", take.ToString());

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var eventList = JsonConvert.DeserializeObject<IEnumerable<Event>>(json);

            return eventList;
        }

        public async Task RemoveEvent(string eventUuid)
        {
            await client.GetStringAsync($"events/{eventUuid}/togglevisibility");
        }

        public async Task<EventSummary> GetEventSummaryAsync(FilterDateRangeType type, string eventUuid)
        {
            var json = await client.GetStringAsync($"events/{eventUuid}/analytics/summary?completed_at_range={type}");
            var eventSummary = JsonConvert.DeserializeObject<EventSummary>(json);

            return eventSummary;
        }
    }
}
