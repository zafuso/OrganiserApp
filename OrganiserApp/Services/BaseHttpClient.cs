using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Services
{
    public class BaseHttpClient
    {
        static string BaseUrl = Config.BaseUrl;
        static string CmSsoAccountGuid = Config.CmSsoAccountGuid;
        static string TechnicalLinkGuid = Config.TechnicalLinkGuid;

        public BaseHttpClient()
        {

        }

        public static HttpClient GetHttpClient()
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
