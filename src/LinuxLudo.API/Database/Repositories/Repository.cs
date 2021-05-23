using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinuxLudo.API.Database.Context;
using LinuxLudo.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LinuxLudo.API.Database.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext Ctx;

        protected Repository(AppDbContext ctx)
        {
            Ctx = ctx;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Ctx.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Ctx.Set<TEntity>().AddRangeAsync(entities);
        }

        public async ValueTask<TEntity> GetByIdAsync(Guid id)
        {
            return await Ctx.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Ctx.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return Ctx.Set<TEntity>().Where(expression);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Ctx.Set<TEntity>().SingleOrDefaultAsync(expression);
        }

        public void Remove(TEntity entity)
        {
            Ctx.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Ctx.Set<TEntity>().RemoveRange(entities);
        }
    }
}