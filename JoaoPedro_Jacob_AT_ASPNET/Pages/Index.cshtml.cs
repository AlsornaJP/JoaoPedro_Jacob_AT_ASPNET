using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Pages.PacotesTuristicos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages;

public class IndexModel : PageModel
{
    private readonly AgenciaContext _context;
    public IndexModel(AgenciaContext context) => _context = context;

    public IList<PacoteInfo> PacotesDestaque { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var pacotes = await _context.PacotesTuristicos
            .Include(p => p.Destinos)
            .Take(3)
            .ToListAsync();

        var reservasPorPacote = await _context.Reservas
            .GroupBy(r => r.PacoteTuristicoId)
            .Select(g => new { PacoteId = g.Key, Total = g.Sum(r => r.Quantidade) })
            .ToDictionaryAsync(x => x.PacoteId, x => x.Total);

        PacotesDestaque = pacotes.Select(p => new PacoteInfo(
            p,
            p.CapacidadeMaxima - (reservasPorPacote.TryGetValue(p.Id, out var total) ? total : 0)
        )).ToList();
    }
}
