using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public partial class ShopStep
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "shop_image")]
        public string ShopImage { get; set; }

        [JsonProperty(PropertyName = "display_order")]
        public int DisplayOrder { get; set; }

        [JsonProperty(PropertyName = "title")]
        public VirtualTranslation Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public VirtualTranslation Description { get; set; }

        [JsonProperty(PropertyName = "shop_step_type_id")]
        public string ShopStepTypeId { get; set; }

        [JsonProperty(PropertyName = "shop_step_display_type_id")]
        public string ShopStepDisplayTypeId { get; set; }

        [JsonProperty(PropertyName = "ticket_types")]
        public List<string> TicketTypes { get; set; }
    }
}
