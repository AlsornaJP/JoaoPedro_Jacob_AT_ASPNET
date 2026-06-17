using System.ComponentModel.DataAnnotations;

namespace JoaoPedro_Jacob_AT_ASPNET.Models;

public class PacoteTuristico
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Título é obrigatório")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Título deve ter entre 3 e 150 caracteres")]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = default!;

    [Required(ErrorMessage = "Data de início é obrigatória")]
    [DataType(DataType.Date)]
    [Display(Name = "Data de Início")]
    public DateTime DataInicio { get; set; }

    [Required]
    [Range(1, 500, ErrorMessage = "Capacidade deve ser entre 1 e 500")]
    [Display(Name = "Capacidade Máxima")]
    public int CapacidadeMaxima { get; set; }

    [Required]
    [Range(0.01, 9999999, ErrorMessage = "Preço deve ser maior que zero")]
    [DataType(DataType.Currency)]
    [Display(Name = "Preço (R$)")]
    public decimal Preco { get; set; }

    [StringLength(600)]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; } = string.Empty;

    public List<Destino> Destinos { get; set; } = new();
}
