using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Clientes;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AgenciaContext _context;
    public IndexModel(AgenciaContext context) => _context = context;

    public IList<Cliente> Cliente { get; set; } = default!;

    public Dictionary<int, int> ReservasPorCliente { get; set; } = new();

    public async Task OnGetAsync()
    {
        Cliente = await _context.Clientes.ToListAsync();

        ReservasPorCliente = await _context.Reservas
            .GroupBy(r => r.ClienteId)
            .Select(g => new { ClienteId = g.Key, Total = g.Count() })
            .ToDictionaryAsync(x => x.ClienteId, x => x.Total);
    }
}
