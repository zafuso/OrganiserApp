using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganiserApp.Enums;

namespace OrganiserApp.Models
{
    public class CustomerTicket
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "status")]
        public CustomerTicketStatusType CustomerTicketStatusType { get; set; }

        [JsonProperty(PropertyName = "is_canceled")]
        public bool IsCanceled { get; set; }

        [JsonProperty(PropertyName = "is_personalized")]
        public bool IsPersonalized { get; set; }

        [JsonProperty(PropertyName = "canceled_at")]
        public string CanceledAt { get; set; }

        [JsonProperty(PropertyName = "personalized_at")]
        public string PersonalizedAt { get; set; }

        [JsonProperty(PropertyName = "customer_data")]
        public CustomerData CustomerData { get; set; }

        [JsonProperty(PropertyName = "serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty(PropertyName = "ticket_type_price_incl_vat")]
        public decimal TicketTypePriceInclVat { get; set; }

        [JsonProperty(PropertyName = "ticket_type_price_with_discount_incl_vat")]
        public decimal TicketTypePriceWithDiscountInclVat { get; set; }
    }
}
