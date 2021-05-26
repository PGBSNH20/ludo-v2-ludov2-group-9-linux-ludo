using System.Collections.Generic;

namespace LinuxLudo.Core.Models
{
    public class Player
    {
        public string Color { get; set; }
        public string Name { get; set; }
        public List<GameToken> Tokens { get; set; }

        public Player(string color, string name)
        {
            Color = color;
            Name = name;
            Tokens = new List<GameToken>()
            {
                new GameToken('A'),
                new GameToken('B'),
                new GameToken('C'),
                new GameToken('D'),
            };
        }
    }
}