using System.ComponentModel.DataAnnotations;
using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using JoaoPedro_Jacob_AT_ASPNET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Reservas;

[Authorize]
public class CreateModel : PageModel
{
    private readonly AgenciaContext _context;
    private readonly ReservaService _reservaService;

    public CreateModel(AgenciaContext context, ReservaService reservaService)
    {
        _context = context;
        _reservaService = reservaService;
    }

    [BindProperty(SupportsGet = true)]
    public int PacoteId { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
    [Display(Name = "Nome completo")]
    public string Nome { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = default!;

    [BindProperty]
    [Required(ErrorMessage = "Telefone é obrigatório")]
    [Phone(ErrorMessage = "Telefone inválido")]
    [Display(Name = "Telefone")]
    public string Telefone { get; set; } = default!;

    [BindProperty]
    [Range(1, 500, ErrorMessage = "Informe quantos lugares deseja reservar (mínimo 1)")]
    [Display(Name = "Quantidade de lugares")]
    public int Quantidade { get; set; } = 1;

    public PacoteTuristico? Pacote { get; set; }
    public int VagasDisponiveis { get; set; }
    public string? ErroVagas { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Pacote = await _context.PacotesTuristicos
            .Include(p => p.Destinos)
            .FirstOrDefaultAsync(p => p.Id == PacoteId);
        if (Pacote == null)
            return RedirectToPage("/PacotesTuristicos/Index");

        Email = User.Identity!.Name!;

        VagasDisponiveis = await CalcularVagasAsync(Pacote);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Pacote = await _context.PacotesTuristicos
            .Include(p => p.Destinos)
            .FirstOrDefaultAsync(p => p.Id == PacoteId);
        if (Pacote == null)
            return RedirectToPage("/PacotesTuristicos/Index");

        Email = User.Identity!.Name!;
        ModelState.Remove(nameof(Email));

        VagasDisponiveis = await CalcularVagasAsync(Pacote);

        if (Pacote.DataInicio <= DateTime.Today)
        {
            ErroVagas = "Este pacote não está mais disponível para reserva (data de saída já passou).";
            return Page();
        }

        if (VagasDisponiveis <= 0)
        {
            ErroVagas = "Este pacote não possui mais vagas disponíveis.";
            return Page();
        }

        if (!ModelState.IsValid)
            return Page();

        if (Quantidade > VagasDisponiveis)
        {
            ErroVagas = $"Este pacote possui apenas {VagasDisponiveis} vaga(s) disponível(is).";
            return Page();
        }

        var jaReservado = await _context.Reservas
            .Include(r => r.Cliente)
            .AnyAsync(r => r.PacoteTuristicoId == PacoteId &&
                           r.Cliente!.Email.ToLower() == Email.ToLower());

        if (jaReservado)
        {
            ModelState.AddModelError(nameof(Email), "Já existe uma reserva para este pacote com este e-mail.");
            return Page();
        }

        var cliente = new Cliente
        {
            Nome = Nome,
            Email = Email,
            Telefone = Telefone
        };
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        await _reservaService.CadastrarReservaAsync(cliente, Pacote, Quantidade);

        return RedirectToPage("./Index");
    }

    private async Task<int> CalcularVagasAsync(PacoteTuristico pacote) =>
        pacote.CapacidadeMaxima - await _context.Reservas
            .Where(r => r.PacoteTuristicoId == pacote.Id)
            .SumAsync(r => r.Quantidade);
}
