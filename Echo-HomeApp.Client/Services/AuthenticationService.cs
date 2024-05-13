using DTOs.RealtorDTOs;
using System.Net;
using System.Net.Http.Json;
using Blazored.SessionStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text.Json;

namespace Services
{
    /// <summary>
    /// Author: Samed Salman
    /// This service is for handling the JWT-token returned from the API
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        // Session service from the NuGet package "Blazored.SessionStorage" contains .NET-references to Javascript code.
        // SessionStorage is used to store data in secure cookies.
        private ISessionStorageService _sessionStorageService;

        // The string where token is saved, initialized as its own name as convention for constant strings
        private const string JWT_KEY = nameof(JWT_KEY);

        // String variable used to cache the token from the sessionStorage to be used by the client
        private string? _jwtCache;

        // Event fires whenever the login status changes
        public event Action<string?>? LoginChange;

        // Inject HttpClient and SessionStorageService
        public AuthenticationService(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
        }


        // Method to catch, read and store JWT
        public async Task<DateTime> LoginAsync(RealtorLoginDTO userLogin)
        {
            HttpResponseMessage response = new();

            try
            {
                // Catch a response message from the API endpoint
                response = await _httpClient.PostAsJsonAsync<RealtorLoginDTO>("https://localhost:7190/api/Realtor/login", userLogin);
            }
            catch (WebException ex)
            {
                // TODO: Show toast message or Blazor Alert
                if (!response.IsSuccessStatusCode)
                {
                    throw new UnauthorizedAccessException("Login failed");
                }
            }

            // Read content of response
            var content = await response.Content.ReadFromJsonAsync<NewUserShowClaimsDTO>();

            if (content == null)
            {
                throw new InvalidDataException();
            }

            // Store the recieved JWT token
            await _sessionStorageService.SetItemAsync(JWT_KEY, content.Token);

            // Invoke the event LoginChange to get user name
            LoginChange.Invoke(GetUserName(content.Token));


            // Create a JwtSecurityTokenHandler to parse token
            var handler = new JwtSecurityTokenHandler();
            // Read the JWT token
            var token = handler.ReadJwtToken(JWT_KEY);
            // Extract the expiration claim
            var expiration = token.ValidTo;

            return expiration;
        }

        // Get JWT from session storage as a cache variable
        public async ValueTask<string> GetJwtAsync()
        {
            // If the cache variable is empty
            if (string.IsNullOrEmpty(_jwtCache))
            {
                // Cache current JWT from sessionStorage
                _jwtCache = await _sessionStorageService.GetItemAsync<string>(JWT_KEY);
            }
            return _jwtCache;
        }

        // Remove JWT from session storage
        public async Task LogoutAsync()
        {
            // Remove token from storage
            await _sessionStorageService.RemoveItemAsync(JWT_KEY);
            // Empty cache
            _jwtCache = null;
            // Invoke Login event and set it to null (to remove user name)
            LoginChange.Invoke(null);
        }

        // Use JWT to retrieve user name
        private static string GetUserName(string token)
        {
            // Local token variable to create and store token from string
            JwtSecurityToken jwt = new JwtSecurityToken(token);
            // Get name value from JWT
            return jwt.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        }
    }
}