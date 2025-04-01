using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using the_learning_lens.Helpers;
using the_learning_lens.Models;

namespace the_learning_lens.Middleware
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
        {
            UserAccount? user = null;

            if (context.User.Identity is ClaimsIdentity identity)
                user = await dbContext.Users.SingleOrDefaultAsync(u => u.UserName == context.User.Identity.Name);

            if (user != null)
                context.Items[ContextItem.UserKey] = user;

            await _next(context);
        }
    }
}
