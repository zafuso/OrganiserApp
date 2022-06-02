using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public partial class Viewport
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "sales_type_id")]
        public string SalesTypeId { get; set; }

        [JsonProperty(PropertyName = "discourage_search_engines")]
        public bool DiscourageSearchEngines { get; set; }

        [JsonProperty(PropertyName = "is_enabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty(PropertyName = "is_visible")]
        public bool IsVisible { get; set; }

        [JsonProperty(PropertyName = "online_from")]
        public string OnlineFrom { get; set; }

        [JsonProperty(PropertyName = "online_until")]
        public string OnlineUntil { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "ticket_Types")]
        public IEnumerable<TicketType> TicketTypes { get; set; }
    }
}
