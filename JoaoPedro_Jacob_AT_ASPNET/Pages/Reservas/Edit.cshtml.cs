using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JoaoPedro_Jacob_AT_ASPNET.Pages.Reservas;

[Authorize]
public class EditModel : PageModel
{
    private readonly AgenciaContext _context;
    public EditModel(AgenciaContext context) => _context = context;

    [BindProperty]
    public Reserva Reserva { get; set; } = default!;

    public SelectList ClientesSelectList { get; set; } = default!;
    public SelectList PacotesSelectList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null) return NotFound();

        Reserva = reserva;
        await CarregarSelectListsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await CarregarSelectListsAsync();
            return Page();
        }

        var existente = await _context.Reservas.FindAsync(Reserva.Id);
        if (existente == null) return NotFound();

        existente.ClienteId = Reserva.ClienteId;
        existente.PacoteTuristicoId = Reserva.PacoteTuristicoId;
        existente.DataReserva = Reserva.DataReserva;
        existente.Quantidade = Reserva.Quantidade;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private async Task CarregarSelectListsAsync()
    {
        ClientesSelectList = new SelectList(await _context.Clientes.ToListAsync(), "Id", "Nome");
        PacotesSelectList = new SelectList(await _context.PacotesTuristicos.ToListAsync(), "Id", "Titulo");
    }
}
