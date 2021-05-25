using LinuxLudo.Web.Domain.Services;

namespace LinuxLudo.Web.Services
{
    public class GameService : IGameService
    {
        private readonly int _gameId;
        public GameService(int gameId)
        {
            _gameId = gameId;
        }

        // Verifies if the specified user can play (the game is not full/player is not already in a game)
        public bool CanPlay(string userName)
        {
            // Fetch data about database from API

            // Check what/how many players are in the game and return result

            return false;
        }
    }
}