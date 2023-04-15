using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mawe.Models;

public class CursoDTO
{
    [Required]
    [DataType(DataType.Text)]
    public string Nome { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public int DuracaoEmMinutos { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [MaxLength(765)]
    public string Descricao { get; set; } = null!;

    [Required]
    [DataType(DataType.ImageUrl)]
    public string CapaUrl { get; set; } = null!;

    [Required]
    [DataType(DataType.Currency)]
    public double Preco { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DataDeCriacao { get; set; }

    [Required]
    public Categorias Categoria { get; set; } 

}
