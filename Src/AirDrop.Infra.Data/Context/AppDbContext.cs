using AirDrop.Domain.Models.Task;
using AirDrop.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using AirDropTask = AirDrop.Domain.Models.Task.AirDropTask;

namespace AirDrop.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AirDropTaskCategory> TaskCategories { get; set; }
        public DbSet<AirDropTask> Tasks { get; set; }
        public DbSet<AirDropTaskImage> TaskImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirDropTaskImage>()
                .HasOne(ti => ti.User)
                .WithMany(u => u.TaskImages)
                .HasForeignKey(ti => ti.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AirDropTaskImage>()
                .HasOne(ti => ti.Task)
                .WithMany(t => t.TaskImages)
                .HasForeignKey(ti => ti.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AirDropTask>()
                .HasOne(t => t.TaskCategory)
                .WithMany(tc => tc.Tasks)
                .HasForeignKey(t => t.TaskCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
