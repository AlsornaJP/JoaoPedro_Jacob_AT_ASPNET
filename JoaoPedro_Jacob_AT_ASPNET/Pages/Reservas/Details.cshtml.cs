using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Reservas;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AgenciaContext _context;
    public DetailsModel(AgenciaContext context) => _context = context;

    public Reserva Reserva { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var reserva = await _context.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.PacoteTuristico)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (reserva == null) return NotFound();
        Reserva = reserva;
        return Page();
    }
}
