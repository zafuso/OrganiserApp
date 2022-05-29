using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class VirtualTranslation
    {
        [JsonProperty(PropertyName = "en")]
        public string En { get; set; }

        [JsonProperty(PropertyName = "nl")]
        public string Nl { get; set; }

        [JsonProperty(PropertyName = "fr")]
        public string Fr { get; set; }

        [JsonProperty(PropertyName = "de")]
        public string De { get; set; }

        [JsonProperty(PropertyName = "ar")]
        public string Ar { get; set; }
    }
}
