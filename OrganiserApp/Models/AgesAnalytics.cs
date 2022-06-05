using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class AgesAnalytics
    {
        [JsonProperty(PropertyName = "content")]
        public List<AgesAnalyticsContent> Content { get; set; }

        [JsonProperty(PropertyName = "date_range")]
        public string DateRange { get; set; }

        [JsonProperty(PropertyName = "total")]
        public string Total { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
    }

    public class AgesAnalyticsContent
    {
        [JsonProperty(PropertyName = "age")]
        public string Age { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }
    }
}
