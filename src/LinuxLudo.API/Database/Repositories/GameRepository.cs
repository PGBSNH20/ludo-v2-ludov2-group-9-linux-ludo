using System;
using System.Threading.Tasks;
using LinuxLudo.API.Database.Context;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Repositories;

namespace LinuxLudo.API.Database.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public async Task<Game> FindByName(string name)
        {
            //return await Ctx.Games.Find();
            throw new NotImplementedException();
        }

        private AppDbContext AppDbContext
        {
            get { return Ctx; }
        }
    }
}