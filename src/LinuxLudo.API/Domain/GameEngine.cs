using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinuxLudo.Core.Models;
using LinuxLudo.Web.Game;
using Microsoft.AspNetCore.SignalR;

namespace LinuxLudo.API
{
    public class GameEngine
    {
        private readonly GameBoard board = new();
        private const int minRoll = 0, maxRoll = 6;
        private readonly List<string> playerColors = new() { "red", "green", "blue", "yellow" };
        private readonly Random random = new();

        public int RollDice()
        {
            // Returns a random number between min and max (inclusive)
            return random.Next(minRoll, maxRoll + 1);
        }

        public string UpdatePlayerTurn(OpenGame game)
        {
            // Starts at one index above the current color and loops until the next color (currently playing/ingame) is found
            for (int i = playerColors.FindIndex(color => color == game.CurrentTurnColor) + 1; i < playerColors.Count; i++)
            {
                // If a player with said color is found 
                if (game.PlayersInGame.Any(player => player.Color == playerColors[i]))
                {
                    return playerColors[i];
                }
            }

            // Defaults to the first color
            return playerColors[0];
        }

        public Dictionary<Player, List<GameToken>> MoveToken(OpenGame game, Player player, GameToken token, int roll)
        {
            Dictionary<Player, List<GameToken>> knockedOutTokens = new();
            for (int i = 0; i < roll; i++)
            {
                int stepIndex = token.TilePos + 1;
                while (!IsWalkable(stepIndex, player, token))
                {
                    stepIndex++;
                }

                token.TilePos = stepIndex;

                // If the player steps on any tokens (knocks them out)
                if (game.PlayersInGame.Any(player => player.Tokens.Any(enemyToken => enemyToken.TilePos == token.TilePos)))
                {
                    foreach (Player enemyPlayer in game.PlayersInGame.Where(p => p.Tokens.Any(eToken => eToken.TilePos == token.TilePos) && p.Name != player.Name))
                    {
                        foreach (GameToken enemyToken in enemyPlayer.Tokens.Where(tok => tok.TilePos == token.TilePos))
                        {
                            // All the tokens that should be knocked out
                            enemyToken.TilePos = -1;
                            enemyToken.InBase = true;
                            enemyToken.MovedFromSpawn = false;

                            if (!knockedOutTokens.ContainsKey(enemyPlayer))
                                knockedOutTokens.Add(enemyPlayer, new List<GameToken>());

                            knockedOutTokens[enemyPlayer].Add(enemyToken);
                        }
                    }
                }
            }

            if (!token.MovedFromSpawn)
            {
                token.MovedFromSpawn = true;
            }

            return knockedOutTokens;
        }

        public GameToken BringOutToken(Player player)
        {
            GameToken token = player.Tokens.First(t => t.InBase);
            token.InBase = false;
            switch (player.Color)
            {
                case "red":
                    token.TilePos = board.Tiles.IndexOf(board.Tiles.First(tile => tile.TileColor == GameTile.GameColor.Red));
                    break;
                case "green":
                    token.TilePos = board.Tiles.IndexOf(board.Tiles.First(tile => tile.TileColor == GameTile.GameColor.Green));
                    break;
                case "blue":
                    token.TilePos = board.Tiles.IndexOf(board.Tiles.First(tile => tile.TileColor == GameTile.GameColor.Blue));
                    break;
                case "yellow":
                    token.TilePos = board.Tiles.IndexOf(board.Tiles.First(tile => tile.TileColor == GameTile.GameColor.Yellow));
                    break;
            }

            return token;
        }

        private bool IsWalkable(int tileIndex, Player player, GameToken token)
        {
            // A tile is walkable if it matches any color, (the players color and did not just spawn at color start), the start of any colored path or the goal tile

            return
            string.Equals(board.Tiles[tileIndex].TileColor.ToString(), "Any", StringComparison.OrdinalIgnoreCase)
            || (string.Equals(board.Tiles[tileIndex].TileColor.ToString(), player.Color, StringComparison.OrdinalIgnoreCase) && token.MovedFromSpawn)
            || tileIndex == board.Tiles.FindLastIndex(tile => tile.TileColor == board.Tiles[tileIndex].TileColor) - 6
            || board.Tiles[tileIndex].TileColor == GameTile.GameColor.Goal;
        }

        public void KnockoutToken(GameToken token)
        {

        }
    }
}