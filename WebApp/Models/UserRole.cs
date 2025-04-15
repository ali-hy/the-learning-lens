using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class UserRole : IdentityRole<long>
    {
        public UserRole() : base() { }
        public UserRole(string roleName) : base(roleName) { }

    }
}
