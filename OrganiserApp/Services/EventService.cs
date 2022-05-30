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
        static readonly HttpClient client = GetHttpClient();

        public async Task<HttpResponseMessage> GetEventListAsync(FilterDateRangeType dateRange, int skip, int take)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"events/list?completed_at_range={dateRange}");
            request.Headers.Add("X-TF-PAGINATION-SKIP", skip.ToString());
            request.Headers.Add("X-TF-PAGINATION-TAKE", take.ToString());

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task RemoveEvent(string EventUuid)
        {
            await client.GetStringAsync($"events/{EventUuid}/togglevisibility");
        }

        public async Task<EventSummary> GetEventSummaryAsync(FilterDateRangeType type, string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/analytics/summary?completed_at_range={type}");
            var eventSummary = JsonConvert.DeserializeObject<EventSummary>(json);

            return eventSummary;
        }

        public async Task<Event> GetEventAsync(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}");
            var selectedEvent = JsonConvert.DeserializeObject<Event>(json);

            return selectedEvent;
        }

        public async Task PutEventAsync(Event selectedEvent)
        {
            var json = JsonConvert.SerializeObject(selectedEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"events/{selectedEvent.Uuid}", content);

            if (!response.IsSuccessStatusCode)
            {
                _ = Application.Current.MainPage.DisplayAlert("Failed", "Failed to update event.", "Cancel");
            }
            else
            {
                _ = Application.Current.MainPage.DisplayAlert("Event saved", "The event has been updated.", "OK");
            }
        }
    }
}
