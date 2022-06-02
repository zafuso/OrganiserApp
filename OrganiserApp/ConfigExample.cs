using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp
{
    public static class ConfigExample
    {
        public const string CmSsoAccountGuid = "00000000-0000-0000-0000-000000000000";
        public const string TechnicalLinkGuid = "00000000-0000-0000-0000-000000000000";

        public static string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8090/ticketingapi/v1.0/" : "http://localhost:8090/ticketingapi/v1.0/";
        public const string HelpcenterUrl = "https://www.cm.com/en-gb/customer-service/#38-184/Ticketing/";
        public const string VisitorUrl = "https://support.ticketing.cm.com/en";
        public const string WhatsAppUrl = "https://api.whatsapp.com/send";
        public const string BaseUrlTicketShop = "https://store.ticketing.cm.com";
        public const string WhatsAppPhoneNumber = "+31761234567";
    }
}
