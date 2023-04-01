using System.ComponentModel.DataAnnotations;

namespace Mawe.Models
{
    public class UsuarioDTO
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Nome { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Sobrenome { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]   
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Papel { get; set; } = null!;
    }
}
