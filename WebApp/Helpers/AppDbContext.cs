using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Helpers
{
    public class AppDbContext : IdentityDbContext<UserAccount, UserRole, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        // New
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Prefab> Prefabs { get; set; }
        public DbSet<ArLesson> ArLesson { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Prefab>(b =>
            {
                // Unique url
                b.HasIndex(b => b.Url)
                .IsUnique();
            });

            builder.Entity<Lesson>();
        }
    }
}
