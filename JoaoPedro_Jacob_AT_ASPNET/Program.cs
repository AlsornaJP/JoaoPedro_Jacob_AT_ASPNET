using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using JoaoPedro_Jacob_AT_ASPNET.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AgenciaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AgenciaContext")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<AgenciaContext>();

builder.Services.AddSingleton<RegistroOperacoesService>();
builder.Services.AddScoped<ReservaService>();

var app = builder.Build();

Reserva.CapacityReached += (sender, e) =>
    Console.WriteLine(
        $"[EVENTO] CapacityReached - O pacote '{e.Pacote.Titulo}' atingiu a capacidade máxima " +
        $"({e.TotalReservas}/{e.Pacote.CapacidadeMaxima}). Novas reservas estão bloqueadas.");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AgenciaContext>();
    context.Database.Migrate();
    DbInitializer.Seed(context);

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    await DbInitializer.SeedUsuarioPadraoAsync(userManager);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
