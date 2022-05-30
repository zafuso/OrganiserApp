using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class TicketPoule
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "capacity")]
        public int Capacity { get; set; }

        [JsonProperty(PropertyName = "is_unlimited")]
        public bool IsUnlimited { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
    }
}
