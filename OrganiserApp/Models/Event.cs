using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    [ObservableObject]
    public partial class Event
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "start_at")]
        public string StartAt { get; set; }

        [JsonProperty(PropertyName = "end_at")]
        public string EndAt { get; set; }

        [JsonProperty(PropertyName = "online_from")]
        public string OnlineFrom { get; set; }

        [JsonProperty(PropertyName = "online_till")]
        public string OnlineUntil { get; set; }

        [JsonProperty(PropertyName = "download_from")]
        public string DownloadFrom { get; set; }

        [JsonProperty(PropertyName = "minutes_to_order")]
        public int MinutesToOrder { get; set; }

        [JsonProperty(PropertyName = "is_free_checkout_allowed")]
        public bool IsFreeCheckoutAllowed { get; set; }

        [JsonProperty(PropertyName = "is_guest_list_using_stock")]
        public bool IsGuestListUsingStock { get; set; }

        [JsonProperty(PropertyName = "is_password_enabled")]
        public bool IsPasswordEnabled { get; set; }

        [JsonProperty(PropertyName = "support_email")]
        public string SupportEmail { get; set; }

        [JsonProperty(PropertyName = "venue_uuid")]
        public string VenueUuid { get; set; }

        [JsonProperty(PropertyName = "purchase_order_number")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty(PropertyName = "is_calendar_enabled")]
        public bool IsCalendarEnabled { get; set; }

        [JsonProperty(PropertyName = "is_ongoing")]
        public bool IsOngoing { get; set; }

        [JsonProperty(PropertyName = "is_downloadable")]
        public bool IsDownloadable { get; set; }

        [JsonProperty(PropertyName = "is_visible")]
        public bool IsVisble { get; set; }

        [JsonProperty(PropertyName = "description")]
        public VirtualTranslation Description { get; set; }

        [JsonProperty(PropertyName = "timezone_id")]
        public int TimezoneId { get; set; }

        [JsonProperty(PropertyName = "summary")]
        public EventSummary EventSummary { get; set; }

    }
}
