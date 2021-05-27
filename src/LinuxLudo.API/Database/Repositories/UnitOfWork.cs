using System.Threading.Tasks;
using LinuxLudo.API.Database.Context;
using LinuxLudo.API.Domain.Repositories;

namespace LinuxLudo.API.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;
        private GameRepository _gameRepository;
        private GameResultRepository _gameResultRepository;

        public UnitOfWork(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IGameRepository Games => _gameRepository = _gameRepository ?? new(_ctx);
        public IGameResultRepository GameResult => _gameResultRepository = _gameResultRepository ?? new(_ctx);

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public async Task<int> CommitAsync()
        {
            return await _ctx.SaveChangesAsync();
        }
    }
}