using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using LinuxLudo.Web.Authentication;

namespace LinuxLudo.Web
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _state;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Fetch a saved token/logged in session
            var token = await _localStorage.GetItemAsync<string>("authToken");

            // If no previous token was found return default (anonymous/unauthorized)
            if (string.IsNullOrWhiteSpace(token))
                return _state;

            // Set the current auth token to the saved token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JWTParser.ParseClaims(token), "jwtAuthType")));
        }

        public void NotifyUserAuthentication(string token)
        {
            // Fetch the current user and its auth state
            var authUser = new ClaimsPrincipal(new ClaimsIdentity(JWTParser.ParseClaims(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authUser));

            // Notify the client that the auth has been changed
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            // Notify the client that the user has logged out
            var authState = Task.FromResult(_state);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}