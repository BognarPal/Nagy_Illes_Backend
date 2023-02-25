using System.ComponentModel.DataAnnotations;

namespace Discite.API.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
