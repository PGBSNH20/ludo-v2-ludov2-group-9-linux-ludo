using LinuxLudo.API.Domain.Enums;
using LinuxLudo.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LinuxLudo.API.Database.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(m => m.Name)
                .IsRequired();
            builder.Property(m => m.MaxPlayers)
                .IsRequired()
                .HasMaxLength(1);
            builder.Property(m => m.State)
                .HasConversion(new EnumToStringConverter<GameStates>())
                .HasDefaultValue(GameStates.Waiting);
        }
    }
}