using Xunit;
using LinuxLudo.API;
using System.ComponentModel;
using LinuxLudo.Core.Models;
using System;
using System.Collections.Generic;
using LinuxLudo.Web.Game;

namespace LinuxLudo.Test.Game
{
    public class GameEngineTest
    {
        private readonly GameEngine engine;
        private readonly GameBoard board;
        public GameEngineTest()
        {
            engine = new GameEngine();
            board = new GameBoard();
        }

        [Fact]
        [Description("Verifies that the dices rolls within the game rules range")]
        public void RollDice_BetweenRange()
        {
            int roll = engine.RollDice();
            Assert.InRange(roll, 0, 6);
        }

        [Fact]
        [Description("Verifies that when a token moves that it doesn't walk up its enemies colored paths")]
        public void MoveToken_EnemyPath()
        {
            OpenGame game = new(Guid.NewGuid(), "Test-game");
            game.PlayersInGame.Add(new Player("Red", "Adam") { Tokens = new List<GameToken>() { new GameToken('A') } });
            game.PlayersInGame[0].Tokens[0].TilePos = 3; // Index 3 = first tile of yellow path

            engine.MoveToken(game, game.PlayersInGame[0], game.PlayersInGame[0].Tokens[0], 5);

            // Verifies that the token did not land on the yellow path, instead on the "any" path
            Assert.Equal(GameTile.GameColor.Any, board.Tiles[game.PlayersInGame[0].Tokens[0].TilePos].TileColor);
        }

        [Fact]
        [Description("Verifies that enemy tokens get knocked out when walked upon")]
        public void MoveToken_WalkEnemy_Knockout()
        {
            OpenGame game = new(Guid.NewGuid(), "Test-game");
            game.PlayersInGame.Add(new Player("Red", "Adam") { Tokens = new List<GameToken>() { new GameToken('A') } });
            game.PlayersInGame.Add(new Player("Blue", "Stephan") { Tokens = new List<GameToken>() { new GameToken('A') } });
            game.PlayersInGame[0].Tokens[0].TilePos = 3; // Index 3 = first tile of yellow path
            game.PlayersInGame[1].Tokens[0].TilePos = 11; // Index 11 = second "any" tile after yellow path

            // Walks 2 tiles (after colored enemy path)
            Dictionary<Player, List<GameToken>> knockedOutTokens = engine.MoveToken(game, game.PlayersInGame[0], game.PlayersInGame[0].Tokens[0], 2);

            // Verifies that the token did not land on the yellow path, instead on the "any" path
            Assert.Contains(knockedOutTokens, pair => pair.Value[0] == game.PlayersInGame[1].Tokens[0]);
        }

        [Fact]
        [Description("Verifies that a token can be brought out when the player just joined")]
        public void NewPlayer_BringToken()
        {
            OpenGame game = new(Guid.NewGuid(), "Test-game");

            // Add the player, their tokens WITHOUT setting any parameters (as is done ingame)
            game.PlayersInGame.Add(new Player("Red", "Adam")
            {
                Tokens = new List<GameToken>() {
                     new GameToken('A'),
                     new GameToken('B'),
                     new GameToken('C'),
                     new GameToken('D'), }
            });

            GameToken token = engine.BringOutToken(game.PlayersInGame[0]);
            Assert.NotNull(token);
        }

        [Fact]
        [Description("Verifies that a token CANNOT be brought out if all tokens are already out of the base")]
        public void Player_BringToken_AllOut()
        {
            OpenGame game = new(Guid.NewGuid(), "Test-game");

            // Add the player, their tokens WITHOUT setting any parameters (as is done ingame)
            game.PlayersInGame.Add(new Player("Red", "Adam")
            {
                Tokens = new List<GameToken>() {
                     new GameToken('A'),
                     new GameToken('B'),
                     new GameToken('C'),
                     new GameToken('D'), }
            });

            game.PlayersInGame[0].Tokens.ForEach(token => token.InBase = false);
            GameToken token = engine.BringOutToken(game.PlayersInGame[0]);
            Assert.Null(token);
        }
    }
}