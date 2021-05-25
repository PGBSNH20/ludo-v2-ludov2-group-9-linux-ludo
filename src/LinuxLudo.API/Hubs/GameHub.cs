using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Repositories;
using LinuxLudo.Web.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace LinuxLudo.API.Hubs
{
    public class GameHub : Hub
    {
        readonly IGameHubRepository _repository;
        public GameHub(IGameHubRepository repository)
        {
            _repository = repository;
        }

        public async Task JoinGame(string username, Guid gameId)
        {
            // Creates a new game if game is not yet active
            if (!_repository.FetchAllGames().Any(game => game.GameId == gameId))
            {
                _repository.AddGame(new OpenGame(gameId));
            }

            OpenGame game = _repository.FetchGameById(gameId);
            _repository.AddPlayer(_repository.FetchGameById(gameId), username);
            await SendGameData(_repository.FetchGameById(gameId));
        }

        private async Task SendGameData(OpenGame game)
        {
            // Send to all playing user the latest game data (players/tokens/positions etc)
            await Clients.All.SendAsync("ReceiveGameData", game);
        }
    }
}