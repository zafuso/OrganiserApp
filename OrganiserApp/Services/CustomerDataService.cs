using Newtonsoft.Json;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class CustomerDataService : BaseHttpClient
    {
        static readonly HttpClient client = GetHttpClient();

        public async Task<CustomerData> PutCustomerDataAsync(CustomerData customerData)
        {
            var json = JsonConvert.SerializeObject(customerData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"customerdata/{customerData.Uuid}", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            _ = Application.Current.MainPage.DisplayAlert("Customer updated", "The customer details have been updated.", "OK");
            return JsonConvert.DeserializeObject<CustomerData>(responseJson);
        }
    }
}
