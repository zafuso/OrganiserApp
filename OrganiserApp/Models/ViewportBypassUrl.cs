using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class ViewportBypassUrl
    {
        [JsonProperty(PropertyName = "queue_token")]
        public string QueueToken { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
