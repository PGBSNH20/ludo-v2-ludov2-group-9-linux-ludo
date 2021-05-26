using System;
using System.Collections.Generic;
using LinuxLudo.Core.Models;

namespace LinuxLudo.Web.Domain.Models
{
    public class GameStatus
    {
        public DateTime StartedDateTime { get; set; }
        public List<Player> Players { get; set; }
        public bool IsCompleted { get; set; }

        public GameStatus()
        {
            Players = new List<Player>();
        }
    }
}