using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.PacotesTuristicos;

[Authorize]
public class EditModel : PageModel
{
    private readonly AgenciaContext _context;
    public EditModel(AgenciaContext context) => _context = context;

    [BindProperty]
    public PacoteTuristico PacoteTuristico { get; set; } = default!;

    [BindProperty]
    public List<int> DestinosSelecionados { get; set; } = new();

    public List<Destino> DestinosDisponiveis { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var pacote = await _context.PacotesTuristicos
            .Include(p => p.Destinos)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (pacote == null) return NotFound();

        PacoteTuristico = pacote;
        DestinosDisponiveis = await _context.Destinos.ToListAsync();
        DestinosSelecionados = pacote.Destinos.Select(d => d.Id).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            DestinosDisponiveis = await _context.Destinos.ToListAsync();
            return Page();
        }

        var existente = await _context.PacotesTuristicos
            .Include(p => p.Destinos)
            .FirstOrDefaultAsync(p => p.Id == PacoteTuristico.Id);
        if (existente == null) return NotFound();

        existente.Titulo = PacoteTuristico.Titulo;
        existente.Descricao = PacoteTuristico.Descricao;
        existente.DataInicio = PacoteTuristico.DataInicio;
        existente.CapacidadeMaxima = PacoteTuristico.CapacidadeMaxima;
        existente.Preco = PacoteTuristico.Preco;

        existente.Destinos.Clear();
        existente.Destinos.AddRange(await _context.Destinos
            .Where(d => DestinosSelecionados.Contains(d.Id))
            .ToListAsync());

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
