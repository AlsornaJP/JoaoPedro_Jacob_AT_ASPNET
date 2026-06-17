using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoaoPedro_Jacob_AT_ASPNET.Data.Configurations;

public class PacoteTuristicoConfiguration : IEntityTypeConfiguration<PacoteTuristico>
{
    public void Configure(EntityTypeBuilder<PacoteTuristico> builder)
    {
        builder.HasMany(p => p.Destinos)
            .WithMany();

        builder.Property(p => p.Preco)
            .HasColumnType("decimal(18,2)");
    }
}
