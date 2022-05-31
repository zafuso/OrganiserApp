using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class TicketType
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "external_id_1")]
        public string ExternalId1 { get; set; }

        [JsonProperty(PropertyName = "external_id_2")]
        public string ExternalId2 { get; set; }

        [JsonProperty(PropertyName = "external_id_3")]
        public string ExternalId3 { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "is_visible")]
        public bool IsVisible { get; set; }

        [JsonProperty(PropertyName = "is_personalizable")]
        public bool IsPersonalizable { get; set; }

        [JsonProperty(PropertyName = "is_selectable_on_guest_list")]
        public bool IsSelectableOnGuestList { get; set; }

        [JsonProperty(PropertyName = "is_deletion_allowed")]
        public bool IsDeletionAllowed { get; set; }

        [JsonProperty(PropertyName = "min_order_amount")]
        public int MinOrderAmount { get; set; }

        [JsonProperty(PropertyName = "max_order_amount")]
        public int MaxOrderAmount { get; set; }

        [JsonProperty(PropertyName = "default_min_order_amount")]
        public int DefaultMinOrderAmount { get; set; }

        [JsonProperty(PropertyName = "default_max_order_amount")]
        public int DefaultMaxOrderAmount { get; set; }

        [JsonProperty(PropertyName = "start_at")]
        public string StartAt { get; set; }

        [JsonProperty(PropertyName = "end_at")]
        public string EndAt { get; set; }

        [JsonProperty(PropertyName = "online_from")]
        public string OnlineFrom { get; set; }

        [JsonProperty(PropertyName = "online_till")]
        public string OnlineUntil { get; set; }

        [JsonProperty(PropertyName = "display_order")]
        public int DisplayOrder { get; set; }

        [JsonProperty(PropertyName = "barcode_expiry_period")]
        public int BarcodeExpiryPeriod { get; set; }

        [JsonProperty(PropertyName = "name")]
        public VirtualTranslation Name { get; set; }

        [JsonProperty(PropertyName = "subtitle")]
        public VirtualTranslation Subtitle { get; set; }

        [JsonProperty(PropertyName = "description")]
        public VirtualTranslation Description { get; set; }

        [JsonProperty(PropertyName = "ticket_category_uuid")]
        public string TicketCategoryUuid { get; set; }

        [JsonProperty(PropertyName = "currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty(PropertyName = "ticket_poule_uuid")]
        public string TicketPouleUuid { get; set; }

        [JsonProperty(PropertyName = "ticket_template_uuid")]
        public string TicketTemplateUuid { get; set; }

        [JsonProperty(PropertyName = "ticket_status_type")]
        public string TicketStatusType { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }

        [JsonProperty(PropertyName = "shop_image")]
        public string ShopImage { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }

        public string Status { get; set; }
    }
}
