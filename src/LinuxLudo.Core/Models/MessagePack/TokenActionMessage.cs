using MessagePack;

namespace LinuxLudo.Core
{
    [MessagePackObject]
    public class TokenActionMessage
    {
        [Key(0)]
        public string Username { get; set; }
        [Key(1)]
        public char TokenIdentifierChar { get; set; }

        public TokenActionMessage() { }
        public TokenActionMessage(string username, char tokenIdentifierChar)
        {
            Username = username;
            TokenIdentifierChar = tokenIdentifierChar;
        }
    }
}