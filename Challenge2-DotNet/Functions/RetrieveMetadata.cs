using Challenge2_DotNet.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Challenge2_DotNet.Functions
{
    public interface IRetrieveMetadata
    {
        Task<string> retrieveMetadata(Request request, string token);
    }
    public class RetrieveMetadata : IRetrieveMetadata
    {
        private readonly HttpClient _httpClient;

        public RetrieveMetadata(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<string> retrieveMetadata(Request request, string token)
        {
            string metadataJson = string.Empty;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("https://management.azure.com/subscriptions/" + request.SubscriptionId + "/resourceGroups/"+ request.ResourceGroup+ "/providers/"+ request.ResourceProvider +"/"+ request.ResourceType+"/"+ request.ResourceName + "?api-version=" + request.ApiVersion);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();

                return responseStr;
            }
            else
            {
                throw new Exception($"Failed to generate token - {response.StatusCode} - {response.ReasonPhrase}");
            }

        }
    }
}
