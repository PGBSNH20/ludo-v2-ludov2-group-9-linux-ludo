using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using System.Collections.Generic;

namespace LinuxLudo.Web.Game.Services
{
    public class GameService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly int _gameId;
        private readonly string _userName;
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:5001/api";

        public GameService(int gameId, string userName)
        {
            _gameId = gameId;
            _userName = userName;
            _client = new();
        }

        public GameService()
        {
            _client = new();
        }

        // Verifies if the specified user can play (the game is not full/player is not already in a game)
        public async Task<bool> CanPlay()
        {
            // Fetch data about database from API
            var body = new
            {
                username = _userName
            };

            // Sends a request to login with provided parameters and fetches the response
            var authResult = await _client.PostAsJsonAsync(API_URL + "/Player/" + _userName, body);
            var authContent = await authResult.Content.ReadAsStringAsync();

            // If the API call wasn't successful
            if (!authResult.IsSuccessStatusCode)
                return false;

            var response = JsonSerializer.Deserialize<UserResponseModel>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var amountOfPlayersInGame = GetGameStatus().Result.Players.Count;

            return await Task.FromResult(!response.InGame && amountOfPlayersInGame < 4);
        }

        public async Task<GameStatus> GetGameStatus()
        {
            // Form data
            var body = new
            {
                gameId = _gameId
            };

            // Sends a request to fetch game data and fetches the response
            var authResult = await _client.PostAsJsonAsync(API_URL + "/Game/" + _gameId, body);
            var authContent = await authResult.Content.ReadAsStringAsync();

            // If the API call wasn't successful
            if (!authResult.IsSuccessStatusCode)
                return null;

            var response = JsonSerializer.Deserialize<GameStatus>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return await Task.FromResult(response);
        }

        public async Task<List<AvailableGame>> FetchAllGames(string authToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var fetchResult = await _client.GetAsync(API_URL + "/Games");

            if (!fetchResult.IsSuccessStatusCode)
            {
                Console.WriteLine("Fetching games failed!");
                return null;
            }

            var resultContent = await fetchResult.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<GamesResponseModel>(resultContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response.Data;
        }

        public async Task RollDice()
        {

        }
    }
}