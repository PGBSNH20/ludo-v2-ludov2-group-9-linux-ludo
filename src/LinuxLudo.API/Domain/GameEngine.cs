using System;
using System.Collections.Generic;
using System.Linq;
using LinuxLudo.Core.Models;
using LinuxLudo.Web.Game;

namespace LinuxLudo.API
{
    public class GameEngine
    {
        private readonly GameBoard board = new GameBoard();
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

        public void MoveToken(Player player, GameToken token, int roll)
        {
            for (int i = 0; i < roll; i++)
            {
                int stepIndex = token.TilePos + 1;
                while (!IsWalkable(stepIndex, player, token))
                {
                    stepIndex++;
                }

                token.TilePos = stepIndex;
            }

            if (!token.MovedFromSpawn)
            {
                token.MovedFromSpawn = true;
            }
        }

        public GameToken BringOutToken(Player player)
        {
            GameToken token = player.Tokens.First(t => t.InBase);
            token.InBase = false;
            token.MovedFromSpawn = false;
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