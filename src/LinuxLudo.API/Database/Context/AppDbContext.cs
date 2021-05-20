using System;
using LinuxLudo.API.Database.Configurations;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinuxLudo.API.Database.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GamePlayerPivot> GamePlayerPivot { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasPostgresExtension("uuid-ossp");
            builder.ApplyConfiguration(new GameConfiguration());
            builder.ApplyConfiguration(new PlayerStatsConfiguration());
            builder.ApplyConfiguration(new GamePlayerPivotConfiguration());

        }
    }
}