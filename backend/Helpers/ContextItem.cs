using the_learning_lens.Models;

namespace the_learning_lens.Helpers
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
