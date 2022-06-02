using Newtonsoft.Json;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class ShopService : BaseHttpClient
    {
        static readonly HttpClient client = GetHttpClient();

        public async Task<IEnumerable<Viewport>> GetViewportsAsync(string EventUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/viewports");
            var viewports = JsonConvert.DeserializeObject<IEnumerable<Viewport>>(json);

            return viewports;
        }

        public async Task<Viewport> PostViewportAsync(Viewport viewport, string EventUuid)
        {
            var json = JsonConvert.SerializeObject(viewport);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"events/{EventUuid}/viewports", content);
            response.EnsureSuccessStatusCode();

            _ = Application.Current.MainPage.DisplayAlert("New shop created", "Your shop has been created.", "OK");

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Viewport>(responseJson);
        }

        public async Task<Viewport> PutViewportAsync(Viewport viewport, string EventUuid)
        {
            var json = JsonConvert.SerializeObject(viewport);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"events/{EventUuid}/viewports/{viewport.Uuid}", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Viewport>(responseJson);
        }

        public async Task<ViewportBypassUrl> PostGenerateBypassUrlAsync(Viewport viewport, string EventUuid)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"events/{EventUuid}/queue/viewports/{viewport.Uuid}/queuetoken/generate");
            request.Headers.Add("X-CM-SSO-EMPLOYEE", 1.ToString());

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ViewportBypassUrl>(responseJson);
        }

        public async Task RemoveViewport(Viewport viewport, string EventUuid)
        {
            var response = await client.DeleteAsync($"events/{EventUuid}/viewports/{viewport.Uuid}");
            response.EnsureSuccessStatusCode();

            _ = Application.Current.MainPage.DisplayAlert("Shop Deleted", $"Shop {viewport.Uri} has been deleted.", "OK");
        }

        public async Task<IEnumerable<ShopStep>> GetShopStepsAsync(string EventUuid, string ViewportUuid)
        {
            var json = await client.GetStringAsync($"events/{EventUuid}/viewports/{ViewportUuid}/shopsteps");
            var shopSteps = JsonConvert.DeserializeObject<IEnumerable<ShopStep>>(json);

            return shopSteps;
        }

        public async Task<ShopStep> PutShopStepAsync(ShopStep shopStep, string EventUuid, string ViewportUuid)
        {
            var json = JsonConvert.SerializeObject(shopStep);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"events/{EventUuid}/viewports/{ViewportUuid}/shopsteps/{shopStep.Uuid}", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ShopStep>(responseJson);
        }
    }
}
