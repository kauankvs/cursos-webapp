using System.ComponentModel.DataAnnotations;

namespace Mawe.Models
{
    public class UsuarioDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Nome { get; set; } = null!;

        [Required]
        public string Sobrenome { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]   
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = null!;

        [Required]
        public int Idade { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Papel { get; set; } = null!;
    }
}
