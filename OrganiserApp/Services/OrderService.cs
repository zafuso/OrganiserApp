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

        public async Task<HttpResponseMessage> GetOrders(string EventUuid, int skip, int take)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"events/{EventUuid}/orders");
            request.Headers.Add("X-TF-PAGINATION-SKIP", skip.ToString());
            request.Headers.Add("X-TF-PAGINATION-TAKE", take.ToString());

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> GetGuestListOrders(string EventUuid, int skip, int take)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"events/{EventUuid}/guestlistorders");
            request.Headers.Add("X-TF-PAGINATION-SKIP", skip.ToString());
            request.Headers.Add("X-TF-PAGINATION-TAKE", take.ToString());

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
