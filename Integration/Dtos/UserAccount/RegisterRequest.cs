using System;

namespace Integration.Dtos.UserAccount
{
    public class RegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public bool IsTrainer {  get; set; }
        public long? TrainerId { get; set; }
    }
}
 