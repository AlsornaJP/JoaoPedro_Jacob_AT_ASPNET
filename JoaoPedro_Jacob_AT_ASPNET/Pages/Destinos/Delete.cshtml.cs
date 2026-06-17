using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Destinos;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly AgenciaContext _context;
    public DeleteModel(AgenciaContext context) => _context = context;

    [BindProperty]
    public Destino Destino { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var destino = await _context.Destinos.FindAsync(id);
        if (destino == null) return NotFound();
        Destino = destino;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var destino = await _context.Destinos.FindAsync(Destino.Id);
        if (destino != null)
        {
            _context.Destinos.Remove(destino);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Index");
    }
}
