using Newtonsoft.Json;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class OrderService : BaseHttpClient
    {
        static readonly HttpClient client = GetHttpClient();

        public async Task<IEnumerable<Order>> GetOrders(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/orders");
            var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json);

            return orders;
        }
    }
}
