using LinuxLudo.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinuxLudo.API.Database.Configurations
{
    public class GameResultConfiguration : IEntityTypeConfiguration<GameResult>
    {
        public void Configure(EntityTypeBuilder<GameResult> builder)
        {
            builder.ToTable("GameResult");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()");
            builder.HasOne(m => m.Game)
                .WithOne(a => a.Result)
                .HasForeignKey<GameResult>(m => m.GameId);
            builder.Property(m => m.Winner)
                .IsRequired();
        }
    }
}