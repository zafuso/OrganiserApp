using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class CitiesAnalytics
    {
        [JsonProperty(PropertyName = "content")]
        public List<CitiesAnalyticsContent> Content { get; set; }

        [JsonProperty(PropertyName = "date_range")]
        public string DateRange { get; set; }

        [JsonProperty(PropertyName = "total")]
        public string Total { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
    }

    public class CitiesAnalyticsContent
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "percentage")]
        public double Percentage { get; set; }
    }
}
