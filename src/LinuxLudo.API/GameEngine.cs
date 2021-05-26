using System;
using LinuxLudo.Core.Models;
using LinuxLudo.Web.Game;

namespace LinuxLudo.API
{
    public class GameEngine
    {
        private readonly GameBoard board;
        private const int minRoll = 0, maxRoll = 6;
        private readonly Random random = new();

        public int RollDice()
        {
            // Returns a random number between min and max (inclusive)
            int roll = random.Next(minRoll, maxRoll + 1);
            return roll;
        }

        public void MoveToken(OpenGame game, GameToken token, int roll)
        {

        }

        public void KnockoutToken(GameToken token)
        {

        }
    }
}