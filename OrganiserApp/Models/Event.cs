using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class Event
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "start_at")]
        public string? StartAt { get; set; }

        [JsonProperty(PropertyName = "end_at")]
        public string? EndAt { get; set; }

        [JsonProperty(PropertyName = "is_calendar_enabled")]
        public bool IsCalendarEnabled { get; set; }

        [JsonProperty(PropertyName = "is_ongoing")]
        public bool IsOngoing { get; set; }

        [JsonProperty(PropertyName = "is_visible")]
        public bool IsVisble { get; set; }

        [JsonProperty(PropertyName = "timezone_id")]
        public int TimezoneId { get; set; }

        public EventSummary EventSummary { get; set; }
    }
}
