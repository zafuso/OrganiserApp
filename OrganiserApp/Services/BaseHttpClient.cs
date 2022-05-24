using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class BaseHttpClient
    {
        static string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8090/ticketingapi/v1.0/" : "http://localhost:8090/ticketingapi/v1.0/";
        static string CmSsoAccountGuid = "6fa19c73-7f4e-4448-a101-7a99fe80c4ca";
        static string TechnicalLinkGuid = "4021c277-1949-420a-a353-0aad5f59fee4";

        public BaseHttpClient()
        {

        }

        public static HttpClient getHttpClient()
        {
            #if DEBUG
            HttpClientHandler handler = GetInsecureHandler();
            HttpClient client = new HttpClient(handler);

            #else
            HttpClient client = new HttpClient();

            #endif

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Add("X-CM-SSO-ACCOUNTGUID", CmSsoAccountGuid);
            client.DefaultRequestHeaders.Add("X-TF-TECHNICALINKGUID", TechnicalLinkGuid);

            return client;
        }

        // Required for HTTP debugging locally
        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}
