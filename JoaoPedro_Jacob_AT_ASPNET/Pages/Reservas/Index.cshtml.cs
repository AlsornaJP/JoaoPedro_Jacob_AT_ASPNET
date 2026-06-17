using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Reservas;

public class IndexModel : PageModel
{
    private readonly AgenciaContext _context;
    public IndexModel(AgenciaContext context) => _context = context;

    public IList<Reserva> Reserva { get; set; } = default!;

    public async Task OnGetAsync() =>
        Reserva = await _context.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.PacoteTuristico)
            .ToListAsync();
}
