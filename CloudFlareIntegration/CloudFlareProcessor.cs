using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace CloudFlareIntegration
{
    internal class CloudFlareProcessor
    {
        private readonly AppConfig Config;

        public CloudFlareProcessor(AppConfig godaddyAppConfig)
        {
            Config = godaddyAppConfig;

        }



        public List<ZoneDto> GetZones()
        {

            string apiPathProjects = "/client/v4/zones";
            var verb = Method.GET;
            var payload = new Dictionary<string, string> { { "per_page", "50" } };
            var response = ConfigureClient(verb, apiPathProjects, payload);
            var content = response.Content; // raw content as string

            var result = JsonConvert.DeserializeObject<RootZoneobject>(content);
            
            return result.result.ToList();

        }

        public List<DnsDto> GetDnsRecordsFromZone(string zoneIdentifier)
        {
            string apiPathProjects = $"/client/v4/zones/{zoneIdentifier}/dns_records";
            var verb = Method.GET;
            var payload = new Dictionary<string, string> { { "per_page", "1000" } };
            var response = ConfigureClient(verb, apiPathProjects, payload);
            var content = response.Content; // raw content as string

            var result = JsonConvert.DeserializeObject<RootDnsObject>(content);

            return result.result.ToList();
          

        }


        private IRestResponse ConfigureClient(Method httpVerb, string resourceMethod, Dictionary<string, string> payload)
        {
            var client = new RestClient(Config.BaseUrl);
            var request = new RestRequest(resourceMethod, httpVerb);
            
            request.AddHeader("X-Auth-Email", Config.email);
            request.AddHeader("X-Auth-Key", Config.api_key);
            //request.AddParameter("Authorization", $"sso-key {Config.api_key}:{Config.api_secret}", ParameterType.HttpHeader);
            //         request.AddHeader("sso-key", $"{Config.api_key}:{Config.api_secret}");


            if (payload != null)
            {
                foreach (var kvp in payload)
                {
                    request.AddParameter(kvp.Key, kvp.Value);
                }
            }

            var response = client.Execute(request);
            return response;

        }


    }
}
