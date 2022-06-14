using OrganiserApp.Enums;
using OrganiserApp.Helpers;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserAppTests
{
    public class OrderTests
    {
        private Order? Order;       
        
        [Fact]
        public void CalculateOrderStatus_Test()
        {
            GenerateTestData();

            var result = OrderHelper.CalculateOrderStatus(Order);

            Assert.Equal(OrderStatusType.COMPLETED, result.Status);
            Assert.Equal("Completed", result.OrderStatus);
            Assert.Equal("check_green.svg", result.StatusIcon);
            Assert.Equal(Color.FromArgb("#3DDC97"), result.StatusTextColor);
        }

        [Fact]
        public void FormatOrder_Test()
        {
            GenerateTestData();

            var viewModel = new OrderOverviewViewModel();
            var result = viewModel.FormatOrder(Order);

            Assert.Equal("John Doe", result.FullName);
            Assert.Equal(3, result.Products);
            Assert.Equal(107.00M, result.TotalBalanceInclVat);
            Assert.IsType<DateTime>(result.Completed);
        }

        public void GenerateTestData()
        {
            var customer = new CustomerData
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var ticketType1 = new OrderTicket
            {
                Name = "Entrance Ticket",
                Amount = 2
            };

            var ticketType2 = new OrderTicket
            {
                Name = "VIP Ticket",
                Amount = 1
            };


            Order = new Order
            {
                Status = OrderStatusType.COMPLETED,
                CustomerData = customer,
                TotalBalanceInclVat = 107,
                CompletedAt = "2022-06-14T10:00:00+00:00",
                TicketTypes = new List<OrderTicket>()
            };

            Order.TicketTypes.Add(ticketType1);
            Order.TicketTypes.Add(ticketType2);
        }
    }
}
