using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public partial class CustomerData
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "mobile")]
        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "address_line_1")]
        public string AddressLine1 { get; set; }

        [JsonProperty(PropertyName = "address_line_1_building_number")]
        public string AddressLine1BuildingNumber { get; set; }

        [JsonProperty(PropertyName = "address_line_2")]
        public string AddressLine2 { get; set; }

        [JsonProperty(PropertyName = "address_line_2_building_number")]
        public string AddressLine2BuildingNumber { get; set; }

        [JsonProperty(PropertyName = "zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "organisation_name")]
        public string OrganisationName { get; set; }

        [JsonProperty(PropertyName = "position_name")]
        public string PositionName { get; set; }

        [JsonProperty(PropertyName = "date_of_birth")]
        public string DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "custom_field_1")]
        public string CustomField1 { get; set; }

        [JsonProperty(PropertyName = "custom_field_2")]
        public string CustomField2 { get; set; }

        [JsonProperty(PropertyName = "custom_field_3")]
        public string CustomField3 { get; set; }

        [JsonProperty(PropertyName = "has_accepted_newsletter")]
        public bool HasAcceptedNewsLetter { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "country_id")]
        public string CountryId { get; set; }
    }
}
