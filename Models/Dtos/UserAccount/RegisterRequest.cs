using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos.UserAccount
{
    public class RegisterRequest
    {
        [MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(64)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(128)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(128)]
        public string Password { get; set; } = string.Empty;

        public DateOnly DOB { get; set; }
        public bool IsTrainer {  get; set; }
        public long? TrainerId { get; set; }
    }
}
