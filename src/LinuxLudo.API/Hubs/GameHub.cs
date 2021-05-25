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
        private readonly IGameHubRepository _repository;
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
            // Adds the player if they don't already exist in the game
            if (!game.PlayersInGame.Any(player => player.Name == username))
            {
                _repository.AddPlayer(_repository.FetchGameById(gameId), username);
            }

            // Updates all clients with the latest player list
            await SendJoinGame(username, _repository.FetchGameById(gameId).PlayersInGame);
        }

        private async Task SendJoinGame(string player, List<Player> players)
        {
            await Clients.All.SendAsync("ReceiveJoinGame", player, players);
        }
    }
}