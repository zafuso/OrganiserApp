using OrganiserApp.Enums;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Helpers
{
    public static class OrderHelper
    {
        public static Order CalculateOrderStatus(Order order)
        {
            switch (order.Status)
            {
                case OrderStatusType.COMPLETED:
                    order.OrderStatus = "Completed";
                    order.StatusIcon = "check_green.svg";
                    order.StatusTextColor = Color.FromArgb("#3DDC97");
                    break;
                case OrderStatusType.REFUNDED:
                    order.OrderStatus = "Refunded";
                    order.StatusIcon = "error_open_red.svg";
                    order.StatusTextColor = Color.FromArgb("#FF495C");
                    break;
                case OrderStatusType.REFUND_PENDING:
                    order.OrderStatus = "Refund pending";
                    order.StatusIcon = "warning_open_orange.svg";
                    order.StatusTextColor = Color.FromArgb("#FFA400");
                    break;
                case OrderStatusType.PARTIALLY_REFUNDED:
                    order.OrderStatus = "Partially Refunded";
                    order.StatusIcon = "warning_open_orange.svg";
                    order.StatusTextColor = Color.FromArgb("#3DDC97");
                    break;
                case OrderStatusType.PARTIALLY_CANCELED:
                    order.OrderStatus = "Partially Canceled";
                    order.StatusIcon = "warning_open_orange.svg";
                    order.StatusTextColor = Color.FromArgb("#3DDC97");
                    break;
                case OrderStatusType.RELEASED:
                    order.OrderStatus = "Released";
                    order.StatusIcon = "error_open_red.svg";
                    order.StatusTextColor = Color.FromArgb("#FF495C");
                    break;
                case OrderStatusType.CANCELED:
                    order.OrderStatus = "Canceled";
                    order.StatusIcon = "error_open_red.svg";
                    order.StatusTextColor = Color.FromArgb("#FF495C");
                    break;
                default: break;
            }
            return order;
        }

        public static Barcode CalculateCustomerTicketStatus(Barcode barcode)
        {
            switch (barcode.CustomerTicket.CustomerTicketStatusType)
            {
                case CustomerTicketStatusType.IN_TRANSFER:
                    barcode.Status = "In Transfer";
                    barcode.StatusIcon = "ticket_block.png";
                    break;
                case CustomerTicketStatusType.IN_UPGRADE:
                    barcode.Status = "In Upgrade";
                    barcode.StatusIcon = "ticket_block.png";
                    break;
                case CustomerTicketStatusType.REFUNDED:
                    barcode.Status = "Refunded";
                    barcode.StatusIcon = "ticket_block.png";
                    break;
                case CustomerTicketStatusType.UPGRADED:
                    barcode.Status = "Upgraded";
                    barcode.StatusIcon = "ticket_block.png";
                    break;
                case CustomerTicketStatusType.TRANSFERRED:
                    barcode.Status = "Transferred";
                    barcode.StatusIcon = "ticket_block.png";
                    break;
                case CustomerTicketStatusType.RESERVED:
                    barcode.Status = "Reserved";
                    barcode.StatusIcon = "ticket_block.png";
                    break;
                case CustomerTicketStatusType.ISSUED:
                    barcode.Status = "Issued";
                    barcode.StatusIcon = "ticket.png";
                    break;
                default: break;
            }

            if (barcode.CustomerTicket.IsCanceled)
            {
                barcode.Status = "Canceled";
                barcode.StatusIcon = "ticket_block.png";
            }

            return barcode;
        }
    }
}
