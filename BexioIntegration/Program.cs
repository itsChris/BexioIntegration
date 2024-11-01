using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace BexioIntegration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Setup configuration and services
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton<AuthService>()
                .AddSingleton<HttpClient>()
                .BuildServiceProvider();

            var authService = serviceProvider.GetService<AuthService>();
            var logger = serviceProvider.GetService<ILogger<Program>>();

            try
            {
                var accessToken = await authService.GetAccessTokenAsync();
                await authService.MakeAuthenticatedRequestAsync(accessToken);
                int i = 0;
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred: {ex.Message}");
            }
        }
    }
}
