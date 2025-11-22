using System.Data.Entity;
using SmartTaskManagement.Models;

namespace SmartTaskManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // REQUIRED: Task assigned TO a user
            modelBuilder.Entity<TaskItem>()
                .HasRequired(t => t.AssignedToUser)
                .WithMany(u => u.TasksAssignedToMe)
                .HasForeignKey(t => t.AssignedToUserId)
                .WillCascadeOnDelete(false);

            // REQUIRED: Task assigned BY a user
            modelBuilder.Entity<TaskItem>()
                .HasRequired(t => t.AssignedByUser)
                .WithMany(u => u.TasksAssignedByMe)
                .HasForeignKey(t => t.AssignedByUserId)
                .WillCascadeOnDelete(false);
        }

    }
}
