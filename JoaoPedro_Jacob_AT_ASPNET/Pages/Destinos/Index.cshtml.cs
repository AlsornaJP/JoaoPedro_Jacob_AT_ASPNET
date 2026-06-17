using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Destinos;

public class IndexModel : PageModel
{
    private readonly AgenciaContext _context;
    public IndexModel(AgenciaContext context) => _context = context;

    public IList<Destino> Destino { get; set; } = default!;

    public async Task OnGetAsync() => Destino = await _context.Destinos.ToListAsync();
}
