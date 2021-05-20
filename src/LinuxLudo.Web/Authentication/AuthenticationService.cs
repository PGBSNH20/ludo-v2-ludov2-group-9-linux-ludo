using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace LinuxLudo.Web.Authentication
{
    // This class is responsible for actually logging in/out through the API
    public class AuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }
    }
}