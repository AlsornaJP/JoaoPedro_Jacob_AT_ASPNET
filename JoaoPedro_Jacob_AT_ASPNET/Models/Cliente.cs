using System.ComponentModel.DataAnnotations;

namespace JoaoPedro_Jacob_AT_ASPNET.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = default!;

    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [StringLength(150)]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [Phone(ErrorMessage = "Telefone inválido")]
    [StringLength(20)]
    [Display(Name = "Telefone")]
    public string Telefone { get; set; } = default!;

    public List<Reserva> Reservas { get; set; } = new();
}
