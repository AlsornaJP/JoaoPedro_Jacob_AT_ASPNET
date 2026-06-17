using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.PacotesTuristicos;

public class DetailsModel : PageModel
{
    private readonly AgenciaContext _context;
    public DetailsModel(AgenciaContext context) => _context = context;

    public PacoteTuristico PacoteTuristico { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var pacote = await _context.PacotesTuristicos
            .Include(p => p.Destinos)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (pacote == null) return NotFound();
        PacoteTuristico = pacote;
        return Page();
    }
}
