using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Data;

public class AgenciaContext : IdentityDbContext<IdentityUser>
{
    public AgenciaContext(DbContextOptions<AgenciaContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Destino> Destinos => Set<Destino>();
    public DbSet<PacoteTuristico> PacotesTuristicos => Set<PacoteTuristico>();
    public DbSet<Reserva> Reservas => Set<Reserva>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgenciaContext).Assembly);
    }
}
