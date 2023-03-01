using System.ComponentModel.DataAnnotations;

namespace Discite.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(32, MinimumLength = 2)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
