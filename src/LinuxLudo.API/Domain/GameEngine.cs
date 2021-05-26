using System;
using System.Collections.Generic;
using System.Linq;
using LinuxLudo.Core.Models;
using LinuxLudo.Web.Game;

namespace LinuxLudo.API
{
    public class GameEngine
    {
        private readonly GameBoard board;
        private const int minRoll = 0, maxRoll = 6;
        private readonly List<string> playerColors = new() { "red", "green", "blue", "yellow" };
        private readonly Random random = new();

        public int RollDice()
        {
            // Returns a random number between min and max (inclusive)
            int roll = random.Next(minRoll, maxRoll + 1);
            return roll;
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

        public void MoveToken(OpenGame game, GameToken token, int roll)
        {

        }

        public void KnockoutToken(GameToken token)
        {

        }
    }
}