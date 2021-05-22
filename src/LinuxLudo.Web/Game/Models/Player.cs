using System.Collections.Generic;

namespace LinuxLudo.Web.Game
{
    public class Player
    {
        public string Color { get; set; }
        public string Name { get; set; }
        public List<GameToken> Tokens { get; set; }
    }
}