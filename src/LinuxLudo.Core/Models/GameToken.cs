using MessagePack;

namespace LinuxLudo.Core.Models
{
    [MessagePackObject]
    public class GameToken
    {
        [Key(5)]
        public int TilePos { get; set; }

        [Key(6)]
        public char IdentifierChar { get; set; }

        [Key(7)]
        public bool InBase { get; set; }

        [Key(8)]
        public bool MovedFromSpawn { get; set; }

        public GameToken(char identifierChar)
        {
            TilePos = -1;
            IdentifierChar = identifierChar;
            InBase = true;
            MovedFromSpawn = false;
        }

        public GameToken() { }
    }
}