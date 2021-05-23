using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Response;

namespace LinuxLudo.API.Domain.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(string id);
        Task<Game> CreateGameAsync(Game game);
        Task DeleteGameAsync(Game game);
    }
}