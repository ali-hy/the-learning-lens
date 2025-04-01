using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using the_learning_lens.Models;

namespace the_learning_lens.Helpers
{
    public class AppDbContext : IdentityDbContext<UserAccount, UserRole, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserTrainer> UserTrainers { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Prefab> Prefabs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAccount>(b =>
            {
                // TrainedBy Relationship
                b.HasMany(u => u.Trainees)
                .WithMany(u => u.Trainers)
                .UsingEntity<UserTrainer>(
                    l => l.HasOne<UserAccount>().WithMany().HasForeignKey(u => u.TraineeId),
                    r => r.HasOne<UserAccount>().WithMany().HasForeignKey(u => u.TrainerId)
                );
            });

            builder.Entity<Module>(b =>
            {
                // CreatedBy Relationship
                b.HasOne(u => u.CreatedBy)
                .WithMany()
                .IsRequired();
            });

            builder.Entity<Prefab>(b =>
            {
                // Unique url
                b.HasIndex(b => b.Url)
                .IsUnique();
            });
        }
    }
}
