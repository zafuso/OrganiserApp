using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class Barcode
    {
        [JsonProperty(PropertyName = "barcode_id")]
        public string BarcodeId { get; set; }

        [JsonProperty(PropertyName = "can_customer_data_be_edited")]
        public bool CanCustomerDataBeEdited { get; set; }

        [JsonProperty(PropertyName = "customer_ticket")]
        public CustomerTicket CustomerTicket { get; set; }

        [JsonProperty(PropertyName = "expire_at")]
        public string ExpireAt { get; set; }

        [JsonProperty(PropertyName = "external_barcode_id")]
        public string ExternalBarcodeId { get; set; }

        [JsonProperty(PropertyName = "service_fee")]
        public decimal ServiceFee { get; set; }

        [JsonProperty(PropertyName = "ticket_type")]
        public BarcodeTicketType TicketType { get; set; }

        public string Status { get; set; }
        public string StatusIcon { get; set; }
    }

    public class BarcodeTicketType
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "name")]
        public VirtualTranslation Name { get; set; }
    }
}
