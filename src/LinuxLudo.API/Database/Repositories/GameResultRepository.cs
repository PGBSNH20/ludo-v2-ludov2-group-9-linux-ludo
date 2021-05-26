using LinuxLudo.API.Database.Context;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Repositories;

namespace LinuxLudo.API.Database.Repositories
{
    public class GameResultRepository : Repository<GameResult>, IGameResultRepository
    {
        public GameResultRepository(AppDbContext ctx) : base(ctx)
        {
        }

        private AppDbContext Ctx => Ctx;
    }
}