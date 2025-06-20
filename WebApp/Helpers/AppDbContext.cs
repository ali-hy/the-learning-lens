using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Archive;

namespace WebApp.Helpers
{
    public class AppDbContext : IdentityDbContext<UserAccount, UserRole, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Archived
        public DbSet<UserTrainer> UserTrainers { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<LearningTask> LearningTasks { get; set; }

        public DbSet<Build> Builds { get; set; }
        public DbSet<BuildPiece> BuildPieces { get; set; }
        public DbSet<BuildPieceSocket> BuildPiecesLink { get; set; }

        public DbSet<Piece> Pieces { get; set; }
        public DbSet<PieceSocket> PieceLinks { get; set; }
        
        // New
        public DbSet<Lesson> Lessons { get; set; }
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
                .WithOne(lt => lt.Module)
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
                b.HasOne(b => b.FirstPiece)
                .WithOne()
                .HasForeignKey<Build>(b => b.FirstPieceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasMany(b => b.Pieces)
                .WithOne(p => p.Build)
                .HasForeignKey(p => p.BuildId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Piece>(b =>
            {
                b.HasMany(p => p.Sockets)
                .WithOne(s => s.Piece)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<BuildPiece>(b =>
            {
                b.HasMany(bp => bp.Sockets)
                .WithOne(s => s.OnBuildPiece)
                .HasForeignKey(s => s.OnBuildPieceId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<BuildPieceSocket>(b =>
            {
                b.HasOne(s => s.HoldingBuildPiece)
                .WithOne(bp => bp.HeldIn)
                .HasForeignKey<BuildPieceSocket>(bp => bp.HoldingBuildPieceId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Lesson>();
        }
        public DbSet<WebApp.Models.ArLesson> ArLesson { get; set; } = default!;
    }
}
