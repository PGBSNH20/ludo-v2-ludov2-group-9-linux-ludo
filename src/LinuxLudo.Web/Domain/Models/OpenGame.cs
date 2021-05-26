using System;
using System.Collections.Generic;

namespace LinuxLudo.Web.Domain.Models
{
    public class OpenGame
    {
        public Guid GameId { get; set; }
        public string CurrentTurnColor { get; set; }
        public List<Player> PlayersInGame { get; set; }

        public OpenGame(Guid gameId)
        {
            GameId = gameId;
            CurrentTurnColor = "red";
            PlayersInGame = new List<Player>();
        }
    }
}