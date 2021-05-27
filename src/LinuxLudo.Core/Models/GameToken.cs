using MessagePack;

namespace LinuxLudo.Core.Models
{
    [MessagePackObject]
    public class GameToken
    {
        [Key(0)]
        public int TilePos { get; set; }

        [Key(1)]
        public char IdentifierChar { get; set; }

        [Key(2)]
        public bool InBase { get; set; }

        [Key(3)]
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