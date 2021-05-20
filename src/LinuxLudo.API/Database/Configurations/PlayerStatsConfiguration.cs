using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinuxLudo.API.Database.Configurations
{
    public class PlayerStatsConfiguration : IEntityTypeConfiguration<PlayerStats>
    {
        public void Configure(EntityTypeBuilder<PlayerStats> builder)
        {
            builder.ToTable("PlayerStats");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()");
            builder.HasOne(m => m.Player)
                .WithOne(a => a.Stats)
                .HasForeignKey<PlayerStats>(m => m.PlayerId);
            builder.Property(m => m.Score)
                .IsRequired();
            builder.Property(m => m.TotalWins)
                .IsRequired();
            builder.Property(m => m.TotalGames)
                .IsRequired();
        }
    }
}