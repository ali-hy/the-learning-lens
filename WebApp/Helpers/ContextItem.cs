using WebApp.Models;

namespace WebApp.Helpers
{
    /// <summary>
    /// Static classes used to access and modify HttpContext.Items
    /// </summary>
    public class ContextItem
    {
        public static readonly string UserKey = "User";


        public static UserAccount? User(HttpContext c)
        {
            return c.Items[UserKey] as UserAccount;
        }
    }
}
