namespace LinuxLudo.Web.Game
{
    public class GameObject
    {
        public int amountOfPlayers;
        public string gameId;

        public GameObject(int amountOfPlayers, string gameId)
        {
            this.amountOfPlayers = amountOfPlayers;
            this.gameId = gameId;
        }
    }
}