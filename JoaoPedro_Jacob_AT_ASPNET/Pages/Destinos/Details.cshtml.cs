using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Destinos;

public class DetailsModel : PageModel
{
    private readonly AgenciaContext _context;
    public DetailsModel(AgenciaContext context) => _context = context;

    public Destino Destino { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var destino = await _context.Destinos.FindAsync(id);
        if (destino == null) return NotFound();
        Destino = destino;
        return Page();
    }
}
