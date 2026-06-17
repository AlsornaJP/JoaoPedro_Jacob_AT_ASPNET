using System.ComponentModel.DataAnnotations;

namespace JoaoPedro_Jacob_AT_ASPNET.Models;

public class Reserva
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cliente é obrigatório")]
    [Display(Name = "Cliente")]
    public int ClienteId { get; set; }

    public Cliente Cliente { get; set; } = default!;

    [Required(ErrorMessage = "Pacote turístico é obrigatório")]
    [Display(Name = "Pacote Turístico")]
    public int PacoteTuristicoId { get; set; }

    public PacoteTuristico PacoteTuristico { get; set; } = default!;

    [Required(ErrorMessage = "Data da reserva é obrigatória")]
    [DataType(DataType.Date)]
    [Display(Name = "Data da Reserva")]
    public DateTime DataReserva { get; set; }

    [Range(1, 500, ErrorMessage = "A quantidade de lugares deve ser entre 1 e 500")]
    [Display(Name = "Quantidade de lugares")]
    public int Quantidade { get; set; } = 1;

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public static event EventHandler<CapacityReachedEventArgs>? CapacityReached;

    public static void RaiseCapacityReached(PacoteTuristico pacote, int totalReservas)
    {
        CapacityReached?.Invoke(null, new CapacityReachedEventArgs(pacote, totalReservas));
    }
}

public class CapacityReachedEventArgs : EventArgs
{
    public PacoteTuristico Pacote { get; }
    public int TotalReservas { get; }

    public CapacityReachedEventArgs(PacoteTuristico pacote, int totalReservas)
    {
        Pacote = pacote;
        TotalReservas = totalReservas;
    }
}
