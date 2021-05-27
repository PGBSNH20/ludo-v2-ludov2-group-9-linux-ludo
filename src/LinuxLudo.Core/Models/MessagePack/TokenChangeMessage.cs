using MessagePack;

namespace LinuxLudo.Core
{
    [MessagePackObject]
    public class TokenChangeMessage
    {
        [Key(0)]
        public string Username { get; set; }
        [Key(1)]
        public char TokenIdentifierChar { get; set; }
        [Key(2)]
        public int TilePos { get; set; }

        public TokenChangeMessage() { }
        public TokenChangeMessage(string username, char tokenIdentifierChar, int tilePos)
        {
            Username = username;
            TokenIdentifierChar = tokenIdentifierChar;
            TilePos = tilePos;
        }
    }
}