using System;
using System.Collections.Generic;

namespace CursosWebApp.Models;

public partial class Curso
{
    public int Id { get; set; }

    public int CriadorId { get; set; }

    public int DuracaoEmMinutos { get; set; }

    public string Descricao { get; set; } = null!;

    public string? CapaUrl { get; set; }

    public DateTime DataDeCriacao { get; set; }

    public virtual Usuario Criador { get; set; } = null!;
}
