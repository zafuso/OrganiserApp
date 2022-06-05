using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class TicketTypesAnalytics
    {
        [JsonProperty(PropertyName = "content")]
        public List<TicketTypeAnalyticsContent> Content  { get; set; }

        [JsonProperty(PropertyName = "date_range")]
        public string DateRange { get; set; }

        [JsonProperty(PropertyName = "total")]
        public TicketTypeAnalyticsTotal Total { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
    }

    public class TicketTypeAnalyticsContent
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "canceled")]
        public int Canceled { get; set; }

        [JsonProperty(PropertyName = "capacity")]
        public int Capacity { get; set; }

        [JsonProperty(PropertyName = "is_unlimited")]
        public bool IsUnlimited { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "reserved")]
        public int Reserved { get; set; }

        [JsonProperty(PropertyName = "stock")]
        public int Stock { get; set; }
    }

    public class TicketTypeAnalyticsTotal
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "reserved")]
        public int Reserved { get; set; }
    }
}
