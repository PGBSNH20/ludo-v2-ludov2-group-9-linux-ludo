using MessagePack;

namespace LinuxLudo.Core
{
    [MessagePackObject]
    public class TokenMoveMessage
    {
        [Key(0)]
        public TokenChangeMessage TokenDetails { get; set; }
        [Key(1)]
        public int Roll { get; set; }

        public TokenMoveMessage() { }
        public TokenMoveMessage(TokenChangeMessage tokenDetails, int roll)
        {
            TokenDetails = tokenDetails;
            Roll = roll;
        }
    }
}