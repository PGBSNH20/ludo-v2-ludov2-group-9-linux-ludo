using LinuxLudo.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinuxLudo.API.Database.Configurations
{
    public class GamePlayerPivotConfiguration : IEntityTypeConfiguration<GamePlayerPivot>
    {
        public void Configure(EntityTypeBuilder<GamePlayerPivot> builder)
        {
            builder.HasAlternateKey(m => new { m.GameId, m.PlayerId })
                .HasName("AlternateKey_GameId_PlayerId");

            builder.Property(m => m.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.HasOne(m => m.Game)
                .WithMany(a => a.Players)
                .HasForeignKey(m => m.GameId);

            builder.HasOne(m => m.Player)
                .WithMany(a => a.Games)
                .HasForeignKey(m => m.PlayerId);
        }
    }
}