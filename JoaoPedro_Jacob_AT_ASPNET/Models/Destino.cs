using System.ComponentModel.DataAnnotations;

namespace JoaoPedro_Jacob_AT_ASPNET.Models;

public class Destino
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cidade é obrigatória")]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Cidade")]
    public string Nome { get; set; } = default!;

    [Required(ErrorMessage = "País é obrigatório")]
    [StringLength(100)]
    [Display(Name = "País")]
    public string Pais { get; set; } = default!;

    [StringLength(500)]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "URL da Foto")]
    public string ImagemUrl { get; set; } = string.Empty;
}
