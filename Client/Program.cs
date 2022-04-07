using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        private static async Task Main()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44358/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "mg.administration",
                ClientSecret = "mg.administration_secret",
                Scope = "read write FeatureScope IdentityServerApi"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            Console.WriteLine("\n\n");

            // Call api

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("https://localhost:44386/WeatherForecast/");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            // Call Different api

            var locationClient = new HttpClient();
            locationClient.SetBearerToken(tokenResponse.AccessToken);
            var locationResponse = await locationClient.GetAsync("https://localhost:44358/api/user/location/4b83fc06-8a50-4936-bf72-41a5baaea9f7");
            if (!locationResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(locationResponse.StatusCode);
            }
            else
            {
                var content = await locationResponse.Content.ReadAsStringAsync();
                Console.WriteLine(JObject.Parse(content));
            }
        }
    }
}