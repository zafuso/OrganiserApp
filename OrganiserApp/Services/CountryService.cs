using Newtonsoft.Json;
using OrganiserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class CountryService : BaseHttpClient
    {
        static readonly HttpClient client = GetHttpClient();

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var json = await client.GetStringAsync("countries");
            var countryList = JsonConvert.DeserializeObject<IEnumerable<Country>>(json);

            return countryList;
        }

        public async Task<IEnumerable<Language>> GetLanguagesAsync()
        {
            var json = await client.GetStringAsync("languages/available");
            var languages = JsonConvert.DeserializeObject<IEnumerable<Language>>(json);

            return languages;
        }
    }
}
