using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Notas;

public class ViewNotesModel : PageModel
{
    private readonly IWebHostEnvironment _env;
    public ViewNotesModel(IWebHostEnvironment env) => _env = env;

    [BindProperty]
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(50, MinimumLength = 1)]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "O conteúdo é obrigatório")]
    [Display(Name = "Conteúdo")]
    public string Conteudo { get; set; } = default!;

    public List<string> Arquivos { get; set; } = new();
    public string? ArquivoSelecionado { get; set; }
    public string? ConteudoSelecionado { get; set; }

    private string PastaNotas => Path.Combine(_env.WebRootPath, "files");

    public void OnGet(string? arquivo)
    {
        CarregarArquivos();

        if (string.IsNullOrEmpty(arquivo))
            return;

        var nomeSeguro = Path.GetFileName(arquivo);
        var caminho = Path.Combine(PastaNotas, nomeSeguro);

        if (nomeSeguro.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) && System.IO.File.Exists(caminho))
        {
            ArquivoSelecionado = nomeSeguro;
            ConteudoSelecionado = System.IO.File.ReadAllText(caminho);
        }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            CarregarArquivos();
            return Page();
        }

        Directory.CreateDirectory(PastaNotas);

        var tituloLimpo = string.Concat(Titulo.Where(c => !Path.GetInvalidFileNameChars().Contains(c))).Trim();
        if (string.IsNullOrEmpty(tituloLimpo))
            tituloLimpo = "nota";

        var nomeArquivo = $"{DateTime.Now:yyyyMMdd_HHmmss}_{tituloLimpo}.txt";
        var caminho = Path.Combine(PastaNotas, nomeArquivo);

        System.IO.File.WriteAllText(caminho, Conteudo);

        return RedirectToPage(new { arquivo = nomeArquivo });
    }

    private void CarregarArquivos()
    {
        if (!Directory.Exists(PastaNotas))
            return;

        Arquivos = Directory.GetFiles(PastaNotas, "*.txt")
            .Select(Path.GetFileName)
            .Where(n => n != null)
            .Select(n => n!)
            .OrderByDescending(n => n)
            .ToList();
    }
}
