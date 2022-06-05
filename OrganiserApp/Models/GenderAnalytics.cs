using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class GenderAnalytics
    {
        [JsonProperty(PropertyName = "content")]
        public List<GenderAnalyticsContent> Content { get; set; }

        [JsonProperty(PropertyName = "date_range")]
        public string DateRange { get; set; }

        [JsonProperty(PropertyName = "total")]
        public string Total { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
    }

    public class GenderAnalyticsContent
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "percentage")]
        public double Percentage { get; set; }
    }
}
