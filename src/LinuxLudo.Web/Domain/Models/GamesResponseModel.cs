
namespace LinuxLudo.Web.Game
{
    public class AvailableGame
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
        public string State { get; set; }
        public int AmountPlaying { get; set; }

        public AvailableGame(string id, string name, int maxPlayers, string state, int amountPlaying)
        {
            Id = id;
            Name = name;
            MaxPlayers = maxPlayers;
            State = state;
            AmountPlaying = amountPlaying;
        }
    }
}