using System.ComponentModel.DataAnnotations;

namespace CursosWebApp.Models
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = null!;
    }
}
