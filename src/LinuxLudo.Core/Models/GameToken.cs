namespace LinuxLudo.Core.Models
{
    public class GameToken
    {
        public int TilePos { get; set; }
        public char IdentifierChar { get; set; }
        public bool InBase { get; set; }

        public GameToken(char identifierChar)
        {
            TilePos = -1;
            IdentifierChar = identifierChar;
            InBase = true;
        }
    }
}