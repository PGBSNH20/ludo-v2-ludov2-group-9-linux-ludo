using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Repositories;
using LinuxLudo.Core.Models;
using Microsoft.AspNetCore.SignalR;
using LinuxLudo.API.Domain.Models;
using MessagePack;

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
            await SendConnectionChanged(gameId.ToString(), MessagePackSerializer.Serialize(username), MessagePackSerializer.Serialize(_repository.FetchGameById(gameId).PlayersInGame));

            // Update the specific player on whose turn it is
            await Clients.Client(Context.ConnectionId).SendAsync("ReceivePlayerTurn", MessagePackSerializer.Serialize(game.CurrentTurnColor));
        }

        public async Task NotifyRollDice(string username, int roll)
        {
            await Clients.Group(_repository.FetchUserById(Context.ConnectionId).JoinedGame.GameId.ToString()).SendAsync("ReceiveRollDice", MessagePackSerializer.Serialize(username),
            MessagePackSerializer.Serialize(roll));
            await Task.Delay(200);
        }

        public async Task MoveToken(string username, char tokenIdentifierChar)
        {
            // Fetches the game the player is in
            OpenGame game = _repository.FetchGameById(_repository.FetchUserById(Context.ConnectionId).JoinedGame.GameId);

            int roll = engine.RollDice();
            if (roll > 0)
            {
                Player player = game.PlayersInGame.First(player => player.Name == username);
                GameToken token = player.Tokens.First(token => token.IdentifierChar == tokenIdentifierChar);
                foreach (KeyValuePair<Player, List<GameToken>> pair in engine.MoveToken(game, player, token, roll))
                {
                    foreach (GameToken enemyToken in pair.Value)
                    {
                        // Broadcast a message for each token that has been knocked out
                        await NotifyTokenKnockout(pair.Key.Name, enemyToken.IdentifierChar);
                        await Task.Delay(1000);
                    }
                }

                // Notify that player has moved
                await Clients.Group(game.GameId.ToString()).SendAsync("ReceiveMoveToken", MessagePackSerializer.Serialize(username),
                MessagePackSerializer.Serialize(token.IdentifierChar), MessagePackSerializer.Serialize(token.TilePos), MessagePackSerializer.Serialize(roll));
            }
            else
            {
                // Notify of 0 roll
                await NotifyRollDice(username, roll);
            }

            await Task.Delay(200);


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
                await Clients.Group(game.GameId.ToString()).SendAsync("ReceiveBringOutToken",
                MessagePackSerializer.Serialize(username),
                MessagePackSerializer.Serialize(token.IdentifierChar),
                MessagePackSerializer.Serialize(token.TilePos));
            }
            else
            {
                await NotifyRollDice(username, roll);
            }

            await Task.Delay(500);

            // Set the turn to the next player
            await UpdatePlayerTurn(game);
        }

        public async Task NotifyTokenKnockout(string tokenHolderName, char tokenIdentifierChar)
        {
            // Fetches the game the player is in
            OpenGame game = _repository.FetchGameById(_repository.FetchUserById(Context.ConnectionId).JoinedGame.GameId);

            // Broadcast which token was knocked out
            await Clients.Group(game.GameId.ToString()).SendAsync("ReceiveTokenKnockout",
            MessagePackSerializer.Serialize(tokenHolderName),
            MessagePackSerializer.Serialize(tokenIdentifierChar));
        }

        private async Task UpdatePlayerTurn(OpenGame game)
        {
            // Sets the current turn to the next player in order
            game.CurrentTurnColor = engine.UpdatePlayerTurn(game);

            // Updates the clients with whose turn it is
            await Clients.Group(game.GameId.ToString()).SendAsync("ReceivePlayerTurn", MessagePackSerializer.Serialize(game.CurrentTurnColor));
        }

        private async Task SendConnectionChanged(string gameId, byte[] player, byte[] players)
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
                await SendConnectionChanged(user.JoinedGame.GameId.ToString(), MessagePackSerializer.Serialize(user.Username), MessagePackSerializer.Serialize(_repository.FetchGameById(user.JoinedGame.GameId).PlayersInGame));

            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}