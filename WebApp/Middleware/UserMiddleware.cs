using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Middleware
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
