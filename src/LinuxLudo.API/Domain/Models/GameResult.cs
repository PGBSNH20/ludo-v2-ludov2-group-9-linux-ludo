using System;

namespace LinuxLudo.API.Domain.Models
{
    public class GameResult
    {
        public Guid Id { get; set; }
        public string Winner { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        
    }
}