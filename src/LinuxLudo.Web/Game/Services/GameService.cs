using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinuxLudo.Web.Game.Services
{
    public class GameService
    {
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
            // TODO REMOVE || ONLY FOR TESTING PURPOSES
            GameStatus TESTING_STATUS = new();
            TESTING_STATUS.Players = new List<Player>()
{
// RED
new Player() {Color = "red", Tokens = new List<GameToken>() {
new GameToken() {TilePos= 63, IdentifierChar = 'A'},
new GameToken() {TilePos= 62, IdentifierChar = 'B'},
new GameToken() {TilePos= 61, IdentifierChar = 'C'},
new GameToken() { TilePos = 60, IdentifierChar = 'D' }}},

// GREEN
new Player() {Color = "green", Tokens = new List<GameToken>() {
new GameToken() {TilePos= 44, IdentifierChar = 'A'},
new GameToken() {TilePos= 43, IdentifierChar = 'B'},
new GameToken() {TilePos= 42, IdentifierChar = 'C'},
new GameToken() { TilePos = 41, IdentifierChar = 'D' }}},

// BLUE
new Player() {Color = "blue", Tokens = new List<GameToken>() {
new GameToken() {TilePos= 25, IdentifierChar = 'A'},
new GameToken() {TilePos= 24, IdentifierChar = 'B'},
new GameToken() {TilePos= 23, IdentifierChar = 'C'},
new GameToken() { TilePos = 22 , IdentifierChar = 'D'}}},

// YELLOW
new Player() {Name = "adam", Color = "yellow", Tokens = new List<GameToken>() {
new GameToken() {TilePos= 6, IdentifierChar = 'A'},
new GameToken() {TilePos= 5, IdentifierChar = 'B'},
new GameToken() {TilePos= 4, IdentifierChar = 'C'},
new GameToken() { TilePos = 3 , IdentifierChar = 'D'}}}
};

            return await Task.FromResult(TESTING_STATUS);

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

        public async Task RollDice()
        {

        }
    }
}