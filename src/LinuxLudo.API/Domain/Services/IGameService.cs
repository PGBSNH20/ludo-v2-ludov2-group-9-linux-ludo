using System.Collections;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Response;

namespace LinuxLudo.API.Domain.Services
{
    public interface IGameService
    {
        Task<BaseResponse> GetAllGames();
        Task<BaseResponse> GetGameById(int id);
        Task<BaseResponse> CreateGame(Game game);
        Task<BaseResponse> DeleteGame(Game game);
    }
}