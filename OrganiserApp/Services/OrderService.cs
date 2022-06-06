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

        public async Task SendGuestListInvitationAsync(string EventUuid, GuestListInvitation Invitation, Language Language)
        {
            var json = JsonConvert.SerializeObject(Invitation);

            using var request = new HttpRequestMessage(HttpMethod.Post, $"events/{EventUuid}/guestlistorders");
            request.Headers.Add("X-TF-PREFERREDLANGUAGEID", Language.Id);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            _ = Application.Current.MainPage.DisplayAlert("Invitation sent", "Your guest has been invited.", "OK");
        }

        public async Task CancelOrderAsync(string EventUuid, Order order)
        {
            var response = await client.GetAsync($"events/{EventUuid}/orders/{order.BatchId}/cancel");
            response.EnsureSuccessStatusCode();

            _ = Application.Current.MainPage.DisplayAlert("Order Cancelled", $"Order {order.BatchId} has been cancelled.", "OK");
        }

        public async Task<IEnumerable<Barcode>> GetBarcodeDetailsAsync(string EventUuid, string OrderId)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/orders/{OrderId}/barcodes");
            var barcodes = JsonConvert.DeserializeObject<IEnumerable<Barcode>>(json);

            return barcodes;
        }
    }
}
