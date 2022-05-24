using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class EventSummary
    {
        [JsonProperty(PropertyName = "date_range")]
        public string? DateRange { get; set; }

        [JsonProperty(PropertyName = "content")]
        public EventSummaryContent Content { get; set; }

        [JsonProperty(PropertyName = "total")]
        public string? Total { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string? UpdatedAt { get; set; }
    }

    public partial class EventSummaryContent
    {
        [JsonProperty(PropertyName = "stock")]
        public int? Stock { get; set; }
        [JsonProperty(PropertyName = "capacity")]
        public int? Capacity { get; set; }
        [JsonProperty(PropertyName = "reserved")]
        public int Reserved { get; set; }
        [JsonProperty(PropertyName = "sales")]
        public int Sales { get; set; }
        [JsonProperty(PropertyName = "sales_percentage")]
        public float? SalesPercentage { get; set; }
        [JsonProperty(PropertyName = "revenue")]
        public decimal Revenue { get; set; }
        [JsonProperty(PropertyName = "currency_id")]
        public string CurrencyId { get; set; }
    }
}
