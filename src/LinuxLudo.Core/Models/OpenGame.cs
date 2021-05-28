using System;
using System.Collections.Generic;
using MessagePack;

namespace LinuxLudo.Core.Models
{
    [MessagePackObject]
    public class OpenGame
    {
        [Key(0)]
        public Guid GameId { get; set; }
        [Key(1)]
        public string CurrentTurnColor { get; set; }
        [Key(2)]
        public string GameName { get; set; }
        [Key(3)]
        public List<Player> PlayersInGame { get; set; }

        public OpenGame() { }
        public OpenGame(Guid gameId, string gameName)
        {
            GameId = gameId;
            GameName = gameName;
            CurrentTurnColor = "red";
            PlayersInGame = new List<Player>();
        }
    }
}