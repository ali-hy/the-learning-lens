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

        public DbSet<UserTrainer> UserTrainers { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<LearningTask> LearningTasks { get; set; }

        public DbSet<Build> Builds { get; set; }
        public DbSet<BuildPiece> BuildPieces { get; set; }
        public DbSet<BuildPieceLink> BuildPiecesLink { get; set; }

        public DbSet<Piece> Pieces { get; set; }
        public DbSet<PieceLink> PieceLinks { get; set; }
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
                    l => l.HasOne(u => u.Trainee).WithMany().HasForeignKey(u => u.TraineeId),
                    r => r.HasOne(u => u.Trainer).WithMany().HasForeignKey(u => u.TrainerId)
                );
            });

            builder.Entity<Module>(b =>
            {
                // CreatedBy Relationship
                b.HasOne(m => m.CreatedBy)
                .WithMany()
                .IsRequired();

                // ExaminationTask Relationship
                b.HasOne(m => m.ExaminationTask)
                .WithOne()
                .HasForeignKey<Module>(m => m.ExaminationTaskId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Prefab>(b =>
            {
                // Unique url
                b.HasIndex(b => b.Url)
                .IsUnique();
            });

            builder.Entity<Build>(b =>
            {
                b.HasMany(b => b.Pieces)
                .WithOne()
                .HasForeignKey(b => b.BuildId);
            });

            builder.Entity<Piece>(b =>
            {
                b.HasMany(p => p.InLinks)
                .WithOne(l => l.InPiece)
                .HasForeignKey(l => l.InPieceId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasMany(p => p.OutLinks)
                .WithOne(l => l.OutPiece)
                .HasForeignKey(l => l.OutPieceId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<BuildPiece>(b =>
            {
                b.HasMany(bp => bp.InLinks)
                .WithOne(l => l.InPiece)
                .HasForeignKey(p => p.InPieceId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasMany(bp => bp.OutLinks)
                .WithOne(l => l.OutPiece)
                .HasForeignKey(bp => bp.OutPieceId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
