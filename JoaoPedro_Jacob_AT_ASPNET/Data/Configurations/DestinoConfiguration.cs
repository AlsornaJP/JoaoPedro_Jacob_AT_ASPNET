using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoaoPedro_Jacob_AT_ASPNET.Data.Configurations;

public class DestinoConfiguration : IEntityTypeConfiguration<Destino>
{
    public void Configure(EntityTypeBuilder<Destino> builder)
    {
        builder.Property(d => d.Nome).IsRequired();
        builder.Property(d => d.Pais).IsRequired();
    }
}
