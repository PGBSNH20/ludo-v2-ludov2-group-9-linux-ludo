using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using LinuxLudo.Web.Authentication;
using LinuxLudo.Web.Domain.Models;
using LinuxLudo.Web.Domain.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Linq;

namespace LinuxLudo.Web.Services
{
    // This class is responsible for actually logging in/out through the API
    public class AuthenticationService : IAuthenticationService
    {
        private const string API_URL = "https://localhost:5001/api/Auth";
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthenticatedUserModel> SignIn(AuthenticationUserModel user)
        {
            // Form/POST call data
            var body = new
            {
                username = user.UserName,
                password = user.Password
            };

            // Sends a request to login with provided parameters and fetches the response
            var authResult = await _client.PostAsJsonAsync(API_URL + "/SignIn", body);

            // If the API call wasn't successful
            if (!authResult.IsSuccessStatusCode)
                return null;

            var authContent = await authResult.Content.ReadAsStringAsync();

            // Deserialize the response as an authenticated object
            var response = JsonSerializer.Deserialize<AuthenticatedUserModel>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            response.AccessToken = JObject.Parse(authContent)["data"]["token"].ToString();

            // Set the currently saved authToken to the newly logged in one
            await _localStorage.SetItemAsync("authToken", response.AccessToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(response.AccessToken);

            // Set the header as the logged in user to mark and display to the website that the user is logged in
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", response.AccessToken);
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

        public async Task<RegisteredUserModel> SignUp(CreateUserModel user)
        {
            // Form/POST call data
            var body = new
            {
                username = user.UserName,
                password = user.Password
            };

            // Sends a request to login with provided parameters and fetches the response
            var authResult = await _client.PostAsJsonAsync(API_URL + "/SignUp", body);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<RegisteredUserModel>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // If the API call wasn't successful
            if (!authResult.IsSuccessStatusCode)
                response.Message = JObject.Parse(authContent)["error"]["message"].ToString();

            return response;
        }
    }
}