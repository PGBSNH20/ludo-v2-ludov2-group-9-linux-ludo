using System;
using LinuxLudo.API.Domain.Models.Auth;

namespace LinuxLudo.API.Domain.Models
{
    public class PlayerStats
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public virtual User Player { get; set; }
        public int Score { get; set; }
        public int TotalGames { get; set; }
        public int TotalWins { get; set; }
    }
}