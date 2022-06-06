using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class GuestListInvitation
    {
        [JsonProperty(PropertyName = "customer_data")]
        public CustomerData CustomerData { get; set; }

        [JsonProperty(PropertyName = "ticket_types")]
        public List<InvitationTicketType> TicketTypes { get; set; }
    }

    public class InvitationTicketType
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        public string Name { get; set; }
        public string Subtitle { get; set; }
        public decimal Price { get; set; } 
    }
}
