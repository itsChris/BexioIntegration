using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BexioIntegration.Services
{
    public abstract class BaseService
    {
        protected readonly HttpClient _httpClient;
        protected readonly ILogger _logger;

        protected BaseService(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected void ConfigureHttpClient(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
