using Newtonsoft.Json;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class VenueService : BaseHttpClient
    {
        static readonly HttpClient client = GetHttpClient();

        public async Task<IEnumerable<Venue>> GetVenuesAsync()
        {
            var json = await client.GetStringAsync("venues");
            var venueList = JsonConvert.DeserializeObject<IEnumerable<Venue>>(json);

            return venueList;
        }

        public async Task<Venue> PostVenueAsync(Venue venue)
        {
            var json = JsonConvert.SerializeObject(venue);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("venues", content);

            if (!response.IsSuccessStatusCode)
            {
                _ = Application.Current.MainPage.DisplayAlert("Failed", "Failed to create location.", "Cancel");
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            var updatedVenue = JsonConvert.DeserializeObject<Venue>(responseJson);

            _ = Application.Current.MainPage.DisplayAlert("Location saved", "The location has been created. Don't forget to save the event!.", "OK");

            return updatedVenue;
        }
    }
}
