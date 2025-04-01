using Microsoft.AspNetCore.Identity;

namespace the_learning_lens.Models
{
    public class UserRole : IdentityRole<long>
    {
        public UserRole() : base() { }
        public UserRole(string roleName) : base(roleName) { }

    }
}
