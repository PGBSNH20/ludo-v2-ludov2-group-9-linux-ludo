using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinuxLudo.Web.Domain.Models;
using LinuxLudo.Web.Game;
using LinuxLudo.Web.Services;

namespace LinuxLudo.Web.Domain.Services
{
    public interface IGameService
    {
        public GameService NewGameService(Guid gameId, string username);
        public Task<bool> CanPlay();
        public Task<GameStatus> GetGameStatus();
        public Task<List<AvailableGame>> FetchAllGames();
    }
}