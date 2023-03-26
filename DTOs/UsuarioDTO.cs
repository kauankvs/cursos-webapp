namespace CursosWebApp.DTOs
{
    public class UsuarioDTO
    {
        public string Username { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public string Sobrenome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;

        public int Idade { get; set; }
    }
}
