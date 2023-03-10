using System.ComponentModel.DataAnnotations;

namespace Discite.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "A felhasználónév minimum 2 és maximum 32 karakter hosszú lehet")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Hibás e-mail cím")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "A jelszó minimum 8 karakter hosszú lehet")]
        public string Password { get; set; }
    }
}
