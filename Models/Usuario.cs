using System;
using System.Collections.Generic;

namespace CursosWebApp.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Sobrenome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public int Idade { get; set; }

    public string Papel { get; set; } = null!;

    public virtual ICollection<Curso> Cursos { get; } = new List<Curso>();
}
