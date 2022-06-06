using OrganiserApp.Enums;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketStatusType = OrganiserApp.Enums.TicketStatusType;

namespace OrganiserApp.Helpers
{
    public static class TicketHelper
    {
        public static TicketType CalculateTicketStatus(TicketType ticket)
        {
            switch (ticket.TicketStatusType)
            {
                case TicketStatusType.ONLINE:
                    ticket.Status = "Online";
                    ticket.StatusIndicator = "status_success.png";
                    break;
                case TicketStatusType.NOT_IN_SALE:
                    ticket.Status = "Currently not in sale";
                    ticket.StatusIndicator = "status_error.png";
                    break;
                case TicketStatusType.DOOR_SALE:
                    ticket.Status = "Offline";
                    ticket.StatusIndicator = "status_error.png";
                    break;
                case TicketStatusType.IN_RESERVATION:
                    ticket.Status = "In reservation";
                    ticket.StatusIndicator = "status_warning.png";
                    break;
                case TicketStatusType.SOLD_OUT:
                    ticket.Status = "Sold out";
                    ticket.StatusIndicator = "status_error.png";
                    break;
                default: break;
            }

            if (ticket.OnlineFrom != null && ticket.OnlineUntil != null)
            {

                var onlineFrom = FormatHelper.FormatISO8601ToDateTime(ticket.OnlineFrom);
                var onlineUntil = FormatHelper.FormatISO8601ToDateTime(ticket.OnlineUntil);

                if (ticket.TicketStatusType == TicketStatusType.NOT_IN_SALE && onlineFrom < DateTime.Now && onlineUntil > DateTime.Now)
                {
                    ticket.Status = $"Online until {FormatHelper.FormatISO8601ToDateString(ticket.OnlineUntil)}";
                    ticket.StatusIndicator = "status_success.png";
                }
            }
            return ticket;
        }
    }
}
