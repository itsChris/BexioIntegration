# Bexio Integration Console Application

This repository contains a .NET Core console application that demonstrates how to authenticate and interact with the Bexio API using the **Authorization Code Grant** flow.

## Table of Contents

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Clone the Repository](#clone-the-repository)
  - [Register Your Application](#register-your-application)
  - [Configuration](#configuration)
  - [Install Dependencies](#install-dependencies)
- [Running the Application](#running-the-application)
- [Code Overview](#code-overview)
- [Notes](#notes)
- [License](#license)

## Introduction

This application demonstrates how to implement the **Authorization Code Grant** flow in a .NET Core console application to authenticate with the Bexio API. It handles user interaction by opening the default web browser for authentication and captures the authorization code using a local HTTP listener.

## Prerequisites

- .NET Core SDK 3.1 or later
- A Bexio developer account
- A registered application in the Bexio developer portal

## Features

- Implements the Authorization Code Grant flow
- Handles user authentication via browser
- Captures authorization code using a local HTTP listener
- Exchanges authorization code for access and refresh tokens
- Makes authenticated API requests to the Bexio API
- Refreshes access tokens using the refresh token

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/itsChris/BexioIntegration.git
cd BexioIntegration
```

### Register Your Application

1. Log in to the [Bexio Developer Portal](https://developer.bexio.com).
2. Navigate to **Apps** and click **Create new app**.
3. Fill in the required details:
   - **App Name**: Your application name.
   - **Redirect URI**: Set this to `http://localhost:5000/callback` (you can choose a different port if desired).
4. After creating the app, note down the **Client ID** and **Client Secret**.

### Configuration

Update the `appsettings.json` file in the project root with your Bexio application details:

```json
{
  "Bexio": {
    "ClientId": "<your_client_id>",
    "ClientSecret": "<your_client_secret>",
    "RedirectUri": "http://localhost:5000/callback",
    "AuthorizationEndpoint": "https://auth.bexio.com/realms/bexio/protocol/openid-connect/auth",
    "TokenEndpoint": "https://auth.bexio.com/realms/bexio/protocol/openid-connect/token",
    "ApiBaseUrl": "https://api.bexio.com/2.0",
    "Scopes": "openid profile contact_show offline_access"
  }
}
```

Replace `<your_client_id>` and `<your_client_secret>` with the values from the Bexio developer portal.

### Install Dependencies

Ensure you have the necessary NuGet packages installed. The required packages are:

- `Microsoft.Extensions.Configuration`
- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.DependencyInjection`
- `Microsoft.Extensions.Logging`
- `Newtonsoft.Json`

You can install them using the following commands:

```bash
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Logging
dotnet add package Newtonsoft.Json
```

## Running the Application

Run the application using the following command:

```bash
dotnet run
```

When you run the application:

1. It will open your default web browser and navigate to the Bexio login page.
2. Log in with your Bexio credentials.
3. Grant the requested permissions to the application.
4. After authorization, you will be redirected to `http://localhost:5000/callback`, and the application will capture the authorization code.
5. The application exchanges the authorization code for access and refresh tokens.
6. It makes an authenticated API request to the Bexio API (e.g., fetching contacts).
7. The response from the API is logged in the console.

## Code Overview

### `AuthService.cs`

This class handles the authentication process:

- **Generating the Authorization URL**: Constructs the URL with the necessary query parameters.
- **Opening the Browser**: Launches the default browser to the authorization URL.
- **Local HTTP Listener**: Starts an `HttpListener` to capture the redirect and extract the authorization code.
- **Exchanging Code for Tokens**: Sends a POST request to the token endpoint to obtain access and refresh tokens.
- **Making Authenticated Requests**: Uses the access token to make API calls to Bexio.

```csharp
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

    public async Task<string> GetAccessTokenAsync()
    {
        // Implementation of the Authorization Code Grant flow
    }

    public async Task MakeAuthenticatedRequestAsync(string accessToken)
    {
        // Implementation of making an authenticated API request
    }

    public async Task<string> RefreshAccessTokenAsync(string refreshToken)
    {
        // Implementation of refreshing the access token
    }
}
```

### `Program.cs`

The main entry point of the application:

- Sets up dependency injection for configuration and logging.
- Creates an instance of `AuthService`.
- Initiates the authentication process.
- Makes authenticated API requests.

```csharp
class Program
{
    static async Task Main(string[] args)
    {
        // Setup configuration and services
        // ...

        var authService = serviceProvider.GetService<AuthService>();
        var logger = serviceProvider.GetService<ILogger<Program>>();

        try
        {
            var accessToken = await authService.GetAccessTokenAsync();
            await authService.MakeAuthenticatedRequestAsync(accessToken);
        }
        catch (Exception ex)
        {
            logger.LogError($"An error occurred: {ex.Message}");
        }
    }
}
```

### `appsettings.json`

Contains the configuration settings for the application:

```json
{
  "Bexio": {
    "ClientId": "<your_client_id>",
    "ClientSecret": "<your_client_secret>",
    "RedirectUri": "http://localhost:5000/callback",
    "AuthorizationEndpoint": "https://auth.bexio.com/realms/bexio/protocol/openid-connect/auth",
    "TokenEndpoint": "https://auth.bexio.com/realms/bexio/protocol/openid-connect/token",
    "ApiBaseUrl": "https://api.bexio.com/2.0",
    "Scopes": "openid profile contact_show offline_access"
  }
}
```

## Notes

- **Local HTTP Listener**: The application uses `HttpListener` to listen on the specified `RedirectUri`. Ensure that the port (e.g., `5000`) is not in use and that your firewall allows incoming connections on this port.

- **State Parameter**: The application uses a `state` parameter to prevent CSRF attacks. It is important to verify that the state received in the redirect matches the one sent in the initial authorization request.

- **Token Storage**: For demonstration purposes, the access and refresh tokens are stored in memory. In a production application, you should securely store these tokens (e.g., encrypted on disk or in a secure database).

- **Scopes**: The scopes requested in `appsettings.json` determine the permissions the application will have. Adjust them according to your needs.

- **Token Refresh**: The `RefreshAccessTokenAsync` method in `AuthService` demonstrates how to refresh the access token using the refresh token. Ensure you handle token expiration appropriately in your application.

- **Error Handling**: The application includes basic error handling and logging. Enhance error handling as needed for your use case.

## License

This project is licensed under the MIT License.

Questions? -> Contact us on https://www.solvia.ch
