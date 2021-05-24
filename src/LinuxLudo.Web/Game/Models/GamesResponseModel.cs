using System.Collections.Generic;

namespace LinuxLudo.Web.Game
{
    public class AvailableGame
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
        public int State { get; set; }

        public int AmountPlaying { get; set; }
    }

    public class GamesResponseModel
    {
        public List<AvailableGame> Data { get; set; }
    }
}