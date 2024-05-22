using Challenge2_DotNet.Model;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;

namespace Challenge2_DotNet.Functions
{
    public interface ITokenGeneration
    {
        Task<string> generateTokenAsync(Request metaDataRequest);
    }
    public class TokenGeneration: ITokenGeneration
    {
        private readonly HttpClient _httpClient;

        public TokenGeneration(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<string> generateTokenAsync(Request request)
        {
            string token = string.Empty;

            var requestUrl = "https://login.microsoftonline.com/" + request.TenantId + "/oauth2/token";

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("client_id", request.ClientId));
            values.Add(new KeyValuePair<string, string>("client_secret", request.ClientSecret));
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            values.Add(new KeyValuePair<string, string>("resource", "https://management.azure.com"));
            values.Add(new KeyValuePair<string, string>("cache-control", "no-cache"));

            var requestBody = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync(requestUrl, requestBody);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<AzureADTokenResponse>(responseStr);
                return content.access_token;
            }
            else
            {
                throw new Exception($"Failed to generate token - {response.StatusCode} - {response.ReasonPhrase}");   
            }
        }
    }
}
