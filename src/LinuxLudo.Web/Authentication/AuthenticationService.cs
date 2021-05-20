using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using LinuxLudo.Web.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace LinuxLudo.Web.Authentication
{
    // This class is responsible for actually logging in/out through the API
    public class AuthenticationService : IAuthenticationService
    {
        private const string API_URL = "https://localhost:5001/api/auth/";
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel user)
        {
            // Form/POST call data
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", user.Email),
                new KeyValuePair<string, string>("password", user.Password)
            });

            // Sends a request to login with provided parameters and fetches the response
            var authResult = await _client.PostAsync(API_URL + "/SignIn", formData);
            var authContent = await authResult.Content.ReadAsStringAsync();

            // If the API call wasn't successful
            if (!authResult.IsSuccessStatusCode)
                return null;

            // Deserialize the response as an authenticated object
            var response = JsonSerializer.Deserialize<AuthenticatedUserModel>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Set the currently saved authToken to the newly logged in one
            await _localStorage.SetItemAsync("authToken", response.AccessToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(response.AccessToken);

            // Set the header as the logged in user to mark and display to the website that the user is logged in
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);
            return response;
        }

        public async Task Logout()
        {
            // Remove the saved token
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            // Remove the logged in header to mark that there is no logged in user
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public bool Equals(AuthenticationService other)
        {
            throw new System.NotImplementedException();
        }
    }
}