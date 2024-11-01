using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace BexioIntegration
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(HttpClient httpClient, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task MakeAuthenticatedRequestAsync(string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Make your API request here
                var response = await _httpClient.GetAsync("https://api.bexio.com/2.0/contact");

                _logger.LogInformation($"Response Status Code: {response.StatusCode}");
                _logger.LogInformation($"Response Content: {await response.Content.ReadAsStringAsync()}");

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error making authenticated request: {ex.Message}");
                throw;
            }
        }

        public async Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            try
            {
                var clientId = _configuration["Bexio:ClientId"];
                var clientSecret = _configuration["Bexio:ClientSecret"];
                var tokenEndpoint = _configuration["Bexio:TokenEndpoint"];

                var tokenRequestBody = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret)
        });

                var tokenResponse = await _httpClient.PostAsync(tokenEndpoint, tokenRequestBody);
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();

                _logger.LogInformation($"Token Refresh Response: {tokenContent}");

                tokenResponse.EnsureSuccessStatusCode();

                var tokenData = JObject.Parse(tokenContent);
                var newAccessToken = tokenData["access_token"]?.ToString();
                var newRefreshToken = tokenData["refresh_token"]?.ToString();

                // Update stored refresh token
                // ...

                return newAccessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error refreshing access token: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                var clientId = _configuration["Bexio:ClientId"];
                var clientSecret = _configuration["Bexio:ClientSecret"];
                var redirectUri = _configuration["Bexio:RedirectUri"];
                var authorizationEndpoint = _configuration["Bexio:AuthorizationEndpoint"];
                var tokenEndpoint = _configuration["Bexio:TokenEndpoint"];
                var scopes = _configuration["Bexio:Scopes"];

                // Step 1: Generate the authorization URL
                var state = Guid.NewGuid().ToString("N");
                var authorizationUrl = $"{authorizationEndpoint}?" +
                    $"response_type=code&" +
                    $"client_id={Uri.EscapeDataString(clientId)}&" +
                    $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
                    $"scope={Uri.EscapeDataString(scopes)}&" +
                    $"state={state}";

                // Step 2: Open the authorization URL in the default browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = authorizationUrl,
                    UseShellExecute = true
                });

                _logger.LogInformation("Opened browser for user authentication.");

                // Step 3: Start a local HTTP listener to catch the redirect
                var httpListener = new HttpListener();
                httpListener.Prefixes.Add(redirectUri + "/");
                httpListener.Start();

                _logger.LogInformation("Listening for authorization code...");

                // Step 4: Wait for the incoming request
                var context = await httpListener.GetContextAsync();

                // Step 5: Extract the authorization code from the query parameters
                var code = context.Request.QueryString["code"];
                var receivedState = context.Request.QueryString["state"];

                // Step 6: Send a response to the browser
                var responseString = "<html><head><meta charset='utf-8'></head><body>You can close this window now.</body></html>";
                var buffer = Encoding.UTF8.GetBytes(responseString);
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();

                httpListener.Stop();

                // Step 7: Verify the state
                if (state != receivedState)
                {
                    throw new InvalidOperationException("State value does not match.");
                }

                _logger.LogInformation("Received authorization code.");

                // Step 8: Exchange the authorization code for an access token
                var tokenRequestBody = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret)
                });

                var tokenResponse = await _httpClient.PostAsync(tokenEndpoint, tokenRequestBody);
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();

                _logger.LogInformation($"Token Response: {tokenContent}");

                tokenResponse.EnsureSuccessStatusCode();

                // Step 9: Parse the access token
                var tokenData = JObject.Parse(tokenContent);
                var accessToken = tokenData["access_token"]?.ToString();
                var refreshToken = tokenData["refresh_token"]?.ToString();

                _logger.LogInformation("Successfully retrieved access token.");

                // Optionally, store the refresh token securely for future use

                return accessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error obtaining access token: {ex.Message}");
                throw;
            }
        }
    }
}
