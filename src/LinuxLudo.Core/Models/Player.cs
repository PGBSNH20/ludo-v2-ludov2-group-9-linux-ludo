using System.Collections.Generic;
using MessagePack;

namespace LinuxLudo.Core.Models
{
    [MessagePackObject]
    public class Player
    {
        [Key(0)]
        public string Color { get; set; }

        [Key(1)]
        public string Name { get; set; }

        [Key(2)]
        public List<GameToken> Tokens { get; set; }

        public Player() { }
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