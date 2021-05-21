using System;
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

        private AppDbContext Ctx => Ctx;
    }
}