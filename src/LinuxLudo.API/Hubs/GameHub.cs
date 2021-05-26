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
        private readonly GameEngine engine = new();
        public GameHub(IGameHubRepository repository) { _repository = repository; }

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

            // Update the specific player on whose turn it is
            await Clients.Client(Context.ConnectionId).SendAsync("ReceivePlayerTurn", game.CurrentTurnColor);
        }

        public async Task NotifyRollDice(string username, int roll)
        => await Clients.Group(_repository.FetchUserById(Context.ConnectionId).JoinedGame.GameId.ToString()).SendAsync("ReceiveRollDice", username, roll);

        public async Task MoveToken(string username, GameToken token)
        {
            // Fetches the game the player is in
            OpenGame game = _repository.FetchGameById(_repository.FetchUserById(Context.ConnectionId).JoinedGame.GameId);

            int roll = engine.RollDice();
            if (roll > 0)
            {
                Player player = game.PlayersInGame.First(player => player.Name == username);
                engine.MoveToken(player, token, roll);

                await Clients.Group(game.GameId.ToString()).SendAsync("ReceiveMoveToken", username, token, roll);
            }
            else
            {
                await NotifyRollDice(username, roll);
            }

            // Set the turn to the next player
            await UpdatePlayerTurn(game);
        }

        public async Task BringOutToken(string username)
        {
            // Fetches the game the player is in
            OpenGame game = _repository.FetchGameById(_repository.FetchUserById(Context.ConnectionId).JoinedGame.GameId);

            // Decides whether or not to bring out a token
            int roll = engine.RollDice();
            if (roll == 6)
            {
                GameToken token = engine.BringOutToken(game.PlayersInGame.First(player => player.Name == username));
                await Clients.Group(game.GameId.ToString()).SendAsync("ReceiveBringOutToken", username, token);
            }
            else
            {
                await NotifyRollDice(username, roll);
            }

            // Set the turn to the next player
            await UpdatePlayerTurn(game);
        }

        private async Task UpdatePlayerTurn(OpenGame game)
        {
            // Sets the current turn to the next player in order
            game.CurrentTurnColor = engine.UpdatePlayerTurn(game);

            // Updates the clients with whose turn it is
            await Clients.Group(game.GameId.ToString()).SendAsync("ReceivePlayerTurn", game.CurrentTurnColor);
        }

        private async Task SendConnectionChanged(string gameId, string player, List<Player> players)
        {
            // Broadcasts to all connected clients (users) that a new player has joined
            await Clients.Group(gameId).SendAsync("ReceiveConnectionChanged", player, players);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUser user = _repository.FetchUserById(Context.ConnectionId);
            if (user?.JoinedGame != null)
            {
                // If it's the user that disconnected's turn, change turn to the next one
                if (user.JoinedGame.CurrentTurnColor == user.JoinedGame.PlayersInGame.First(player => player.Name == user.Username).Color)
                {
                    await UpdatePlayerTurn(user.JoinedGame);
                }

                // Removes the player from their game
                _repository.RemovePlayer(user.JoinedGame, user.Username);

                // Update the clients that user has left
                await SendConnectionChanged(user.JoinedGame.GameId.ToString(), user.Username, _repository.FetchGameById(user.JoinedGame.GameId).PlayersInGame);

            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}