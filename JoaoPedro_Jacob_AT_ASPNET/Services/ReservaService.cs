using JoaoPedro_Jacob_AT_ASPNET.Data;
using JoaoPedro_Jacob_AT_ASPNET.Models;

namespace JoaoPedro_Jacob_AT_ASPNET.Services;

public class ReservaService
{
    private readonly AgenciaContext _context;
    private readonly RegistroOperacoesService _registro;

    public ReservaService(AgenciaContext context, RegistroOperacoesService registro)
    {
        _context = context;
        _registro = registro;
    }

    public async Task<Reserva> CadastrarReservaAsync(Cliente cliente, PacoteTuristico pacote, int quantidade)
    {
        var reserva = new Reserva
        {
            ClienteId = cliente.Id,
            Cliente = cliente,
            PacoteTuristicoId = pacote.Id,
            PacoteTuristico = pacote,
            DataReserva = DateTime.Today,
            Quantidade = quantidade
        };

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        _registro.Registrar($"Reserva criada: cliente '{cliente.Nome}', pacote '{pacote.Titulo}', {quantidade} lugar(es).");

        var lugaresOcupados = _context.Reservas
            .Where(r => r.PacoteTuristicoId == pacote.Id)
            .Sum(r => r.Quantidade);
        if (lugaresOcupados >= pacote.CapacidadeMaxima)
            Reserva.RaiseCapacityReached(pacote, lugaresOcupados);

        return reserva;
    }
}
