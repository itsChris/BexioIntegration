using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace BexioIntegration
{
    public class BexioService
    {
        private readonly AuthService _authService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BexioService> _logger;

        public BexioService(AuthService authService, HttpClient httpClient, ILogger<BexioService> logger)
        {
            _authService = authService;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task ListAllContactsAsync()
        {
            try
            {
                var accessToken = await _authService.GetAccessTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync("https://api.bexio.com/2.0/contact");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var contacts = JArray.Parse(content);

                foreach (var contact in contacts)
                {
                    Console.WriteLine($"ID: {contact["id"]}, Name: {contact["name"]}");
                }

                _logger.LogInformation("Contacts retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error listing contacts: {ex.Message}");
            }
        }
    }
}
