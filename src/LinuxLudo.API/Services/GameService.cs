using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Repositories;
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
        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            var games = await _unitOfWork.Games.GetAllAsync();
            if (!games.Any())
                return null;

            return games;
        }

        public async Task<Game> GetGameByIdAsync(string id)
        {
            var isValid = Guid.TryParse(id, out var guid);
            if (!isValid)
                return null;

            return await _unitOfWork.Games.GetByIdAsync(guid);
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            var exists = _unitOfWork.Games.Find(t => t.Name == game.Name).FirstOrDefault();
            if (exists != null)
                return new Game();

            await _unitOfWork.Games.AddAsync(game);
            await _unitOfWork.CommitAsync();
            var res = _unitOfWork.Games.Find(t => t.Name == game.Name).FirstOrDefault();
            return res;
        }

        public async Task DeleteGameAsync(Game game)
        {
            _unitOfWork.Games.Remove(game);
            await _unitOfWork.CommitAsync();
        }
    }
}