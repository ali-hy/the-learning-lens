using System.ComponentModel.DataAnnotations;

namespace the_learning_lens.Dtos.UserAccount
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(128)]
        public string Password { get; set; } = string.Empty;
    }
}
