using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Repositories;
using LinuxLudo.API.Domain.Response;
using LinuxLudo.API.Domain.Services;

namespace LinuxLudo.API.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse> GetAllGames()
        {
            var games = await _unitOfWork.Games.GetAllAsync();
            return new SuccessResponse("Success", 200).Respond(games);
        }

        public async Task<BaseResponse> GetGameById(int id)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(id);
            return new SuccessResponse("Success", 200).Respond(game);
        }

        public async Task<BaseResponse> CreateGame(Game game)
        {
            await _unitOfWork.Games.AddAsync(game);
            await _unitOfWork.CommitAsync();
            return new SuccessResponse("Game created", 201).Respond();
        }

        public async Task<BaseResponse> DeleteGame(Game game)
        {
            _unitOfWork.Games.Remove(game);
            await _unitOfWork.CommitAsync();
            return new SuccessResponse($"Game {game.Id}, deleted.", 200).Respond();
        }
    }
}