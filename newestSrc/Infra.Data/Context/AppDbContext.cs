using Domain.Models.Category;
using Domain.Models.Friend;
using Domain.Models.Label;
using Domain.Models.Task;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CategoryTaskModel> CategoryTasks { get; set; }
        public DbSet<LabelImageModel> LabelImages { get; set; }
        public DbSet<TaskImageModel> TaskImages { get; set; }
        public DbSet<TaskEarnModel> TaskEarns { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<FriendsModel> Friends { get; set; }
    }
}
