using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Repositories;
using LinuxLudo.Core.Models;
using Microsoft.AspNetCore.SignalR;
using LinuxLudo.API.Domain.Models;

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
            if (game.PlayersInGame.All(player => player.Name != username))
            {
                _repository.AddPlayer(_repository.FetchGameById(gameId), username);

                // Adds a new connected user that is linked to the selected game
                _repository.ConnectUser(new ConnectedUser(username, Context.ConnectionId, game));
            }

            // Add the new player to the joined game's group
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

            // Updates all clients with the latest player list
            await SendConnectionChanged(gameId.ToString(), username, _repository.FetchGameById(gameId).PlayersInGame);
        }

        private async Task SendConnectionChanged(string gameId, string player, List<Player> players)
        {
            // Broadcasts to all connected clients (users) that a new player has joined
            await Clients.Group(gameId).SendAsync("ReceiveConnectionChanged", player, players);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUser user = _repository.FetchUserById(Context.ConnectionId);
            if (user != null)
            {
                // Removes the player from their game
                _repository.RemovePlayer(user.JoinedGame, user.Username);

                // Update the clients that user has left
                await SendConnectionChanged(user.JoinedGame.GameId.ToString(), user.Username, _repository.FetchGameById(user.JoinedGame.GameId).PlayersInGame);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}