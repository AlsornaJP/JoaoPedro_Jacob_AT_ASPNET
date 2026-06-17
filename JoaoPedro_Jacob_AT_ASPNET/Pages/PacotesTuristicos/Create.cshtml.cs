using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.PacotesTuristicos;

[Authorize]
public class CreateModel : PageModel
{
    private readonly AgenciaContext _context;
    public CreateModel(AgenciaContext context) => _context = context;

    [BindProperty]
    public PacoteTuristico PacoteTuristico { get; set; } = new();

    [BindProperty]
    public List<int> DestinosSelecionados { get; set; } = new();

    public List<Destino> DestinosDisponiveis { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        DestinosDisponiveis = await _context.Destinos.ToListAsync();
        PacoteTuristico.DataInicio = DateTime.Today.AddMonths(1);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            DestinosDisponiveis = await _context.Destinos.ToListAsync();
            return Page();
        }

        PacoteTuristico.Destinos = await _context.Destinos
            .Where(d => DestinosSelecionados.Contains(d.Id))
            .ToListAsync();

        _context.PacotesTuristicos.Add(PacoteTuristico);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
