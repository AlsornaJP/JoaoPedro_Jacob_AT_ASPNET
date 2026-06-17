using System.ComponentModel.DataAnnotations;
using JoaoPedro_Jacob_AT_ASPNET.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.RegistroOperacoes;

public class IndexModel : PageModel
{
    private readonly RegistroOperacoesService _registro;
    public IndexModel(RegistroOperacoesService registro) => _registro = registro;

    [BindProperty]
    [Required(ErrorMessage = "Informe uma mensagem")]
    public string Mensagem { get; set; } = default!;

    public IReadOnlyList<string> LogsMemoria => _registro.Memoria;

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        _registro.Registrar(Mensagem);

        return RedirectToPage();
    }
}
