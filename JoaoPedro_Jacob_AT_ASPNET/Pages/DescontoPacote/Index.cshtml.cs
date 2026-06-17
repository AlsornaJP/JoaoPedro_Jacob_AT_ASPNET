using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Delegates;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.DescontoPacote;

public class IndexModel : PageModel
{
    private readonly AgenciaContext _context;
    public IndexModel(AgenciaContext context) => _context = context;

    [BindProperty]
    public int PacoteId { get; set; }

    public PacoteTuristico? PacoteSelecionado { get; set; }
    public decimal? PrecoComDesconto { get; set; }
    public SelectList PacotesSelectList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        await CarregarPacotesAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await CarregarPacotesAsync();

        PacoteSelecionado = await _context.PacotesTuristicos.FirstOrDefaultAsync(p => p.Id == PacoteId);
        if (PacoteSelecionado == null)
        {
            ModelState.AddModelError(string.Empty, "Selecione um pacote válido.");
            return Page();
        }

        CalculateDelegate aplicarDesconto = preco => preco * 0.90m;
        PrecoComDesconto = aplicarDesconto(PacoteSelecionado.Preco);

        return Page();
    }

    private async Task CarregarPacotesAsync() =>
        PacotesSelectList = new SelectList(await _context.PacotesTuristicos.ToListAsync(), "Id", "Titulo");
}
