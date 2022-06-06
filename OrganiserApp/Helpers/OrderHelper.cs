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
                    order.OrderStatus = "Partially Cancelled";
                    order.StatusIcon = "warning_open_orange.svg";
                    order.StatusTextColor = Color.FromArgb("#3DDC97");
                    break;
                case OrderStatusType.RELEASED:
                    order.OrderStatus = "Released";
                    order.StatusIcon = "error_open_red.svg";
                    order.StatusTextColor = Color.FromArgb("#FF495C");
                    break;
                case OrderStatusType.CANCELED:
                    order.OrderStatus = "Cancelled";
                    order.StatusIcon = "error_open_red.svg";
                    order.StatusTextColor = Color.FromArgb("#FF495C");
                    break;
                default: break;
            }
            return order;
        }
    }
}
