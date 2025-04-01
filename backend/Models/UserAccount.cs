using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace the_learning_lens.Models
{
    public class UserAccount: IdentityUser<long>
    {
        [MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(64)]
        public string LastName {  get; set; } = string.Empty;

        public DateOnly DOB { get; set; }

        public List<UserAccount>? Trainers { get; set; } 
        public List<UserAccount>? Trainees { get; set; } 
    }
}
