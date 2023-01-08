using System.ComponentModel.DataAnnotations;
using WebApi.Domain.Cargo;

namespace WebApi.Domain.Colaborador;

public class ColaboradorInputModel
{

    [Required]
    public string Nome { get; set; }
    
    [Required]
    public int? Matricula { get; set; }
    
    [Required]
    public DateTime? DataNascimento { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    [Required] public Cargo.Cargo? Cargo { get; set; }

}