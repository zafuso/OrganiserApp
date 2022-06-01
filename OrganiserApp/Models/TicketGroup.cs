using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public class TicketGroup : List<TicketType>
    {
        public TicketCategory TicketCategory { get; private set; }
        public TicketGroup(TicketCategory ticketCategory, List<TicketType> tickets) : base(tickets)
        {
            TicketCategory = ticketCategory;
        }
    }
}
