using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.PacotesTuristicos;

public record PacoteInfo(PacoteTuristico Pacote, int VagasDisponiveis)
{
    public bool Disponivel => VagasDisponiveis > 0 && Pacote.DataInicio > DateTime.Today;
}

public class IndexModel : PageModel
{
    private readonly AgenciaContext _context;
    public IndexModel(AgenciaContext context) => _context = context;

    public IList<PacoteInfo> Pacotes { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var pacotes = await _context.PacotesTuristicos
            .Include(p => p.Destinos)
            .ToListAsync();

        var reservasPorPacote = await _context.Reservas
            .GroupBy(r => r.PacoteTuristicoId)
            .Select(g => new { PacoteId = g.Key, Total = g.Sum(r => r.Quantidade) })
            .ToDictionaryAsync(x => x.PacoteId, x => x.Total);

        Pacotes = pacotes.Select(p => new PacoteInfo(
            p,
            p.CapacidadeMaxima - (reservasPorPacote.TryGetValue(p.Id, out var total) ? total : 0)
        )).ToList();
    }
}
