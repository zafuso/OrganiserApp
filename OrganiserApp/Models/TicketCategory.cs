using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class TicketCategory
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "display_order")]
        public int DisplayOrder { get; set; }

        [JsonProperty(PropertyName = "is_initialized")]
        public bool IsInitialized { get; set; }

        [JsonProperty(PropertyName = "is_default")]
        public bool IsDefault { get; set; }

        [JsonProperty(PropertyName = "max_order_amount")]
        public int MaxOrderAmount { get; set; }

        [JsonProperty(PropertyName = "name")]
        public VirtualTranslation Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public VirtualTranslation Description { get; set; }

        [JsonProperty(PropertyName = "shop_image")]
        public string ShopImage { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
    }
}
