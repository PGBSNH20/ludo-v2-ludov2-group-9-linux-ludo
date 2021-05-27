using MessagePack;

namespace LinuxLudo.Core
{
    [MessagePackObject]
    public class PlayerRollMessage
    {
        [Key(0)]
        public string Username { get; set; }
        [Key(1)]
        public int Roll { get; set; }

        public PlayerRollMessage() { }
        public PlayerRollMessage(string username, int roll)
        {
            Username = username;
            Roll = roll;
        }
    }
}