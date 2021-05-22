using System;
using System.Collections;
using System.Collections.Generic;
using LinuxLudo.API.Domain.Enums;

namespace LinuxLudo.API.Domain.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
        public GameStates State { get; set; }
        public virtual ICollection<GamePlayerPivot> Players { get; set; }
    }
}