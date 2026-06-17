using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.SimulacaoReserva;

public class IndexModel : PageModel
{
    private readonly AgenciaContext _context;
    public IndexModel(AgenciaContext context) => _context = context;

    [BindProperty]
    public int PacoteId { get; set; }

    [BindProperty]
    public int Quantidade { get; set; }

    public decimal? Total { get; set; }
    public PacoteTuristico? PacoteSelecionado { get; set; }
    public SelectList PacotesSelectList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        await CarregarSelectListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await CarregarSelectListAsync();

        PacoteSelecionado = await _context.PacotesTuristicos.FirstOrDefaultAsync(p => p.Id == PacoteId);

        if (PacoteSelecionado == null || Quantidade <= 0)
        {
            ModelState.AddModelError(string.Empty, "Selecione um pacote e informe uma quantidade válida.");
            return Page();
        }

        Func<int, int, decimal> calcularValorTotal = (quantidade, preco) => quantidade * (decimal)preco;

        Total = calcularValorTotal(Quantidade, (int)PacoteSelecionado.Preco);

        return Page();
    }

    private async Task CarregarSelectListAsync() =>
        PacotesSelectList = new SelectList(await _context.PacotesTuristicos.ToListAsync(), "Id", "Titulo");
}
