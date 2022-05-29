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
        static readonly HttpClient client = getHttpClient();

        public async Task<IEnumerable<Venue>> GetVenuesAsync()
        {
            var json = await client.GetStringAsync("venues");
            var venueList = JsonConvert.DeserializeObject<IEnumerable<Venue>>(json);

            return venueList;
        }
    }
}
