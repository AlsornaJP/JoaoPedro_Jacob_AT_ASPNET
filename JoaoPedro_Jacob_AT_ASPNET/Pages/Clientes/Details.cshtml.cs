using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Clientes;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AgenciaContext _context;
    public DetailsModel(AgenciaContext context) => _context = context;

    public Cliente Cliente { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var cliente = await _context.Clientes
            .Include(c => c.Reservas)
                .ThenInclude(r => r.PacoteTuristico)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (cliente == null) return NotFound();

        Cliente = cliente;
        return Page();
    }
}
