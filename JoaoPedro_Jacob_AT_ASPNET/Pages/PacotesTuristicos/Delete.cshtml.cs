using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.PacotesTuristicos;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly AgenciaContext _context;
    public DeleteModel(AgenciaContext context) => _context = context;

    [BindProperty]
    public PacoteTuristico PacoteTuristico { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var pacote = await _context.PacotesTuristicos.FindAsync(id);
        if (pacote == null) return NotFound();
        PacoteTuristico = pacote;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var pacote = await _context.PacotesTuristicos.FindAsync(PacoteTuristico.Id);
        if (pacote != null)
        {
            _context.PacotesTuristicos.Remove(pacote);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Index");
    }
}
