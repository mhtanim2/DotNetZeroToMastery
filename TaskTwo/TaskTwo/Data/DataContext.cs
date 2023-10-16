using Microsoft.EntityFrameworkCore;
using TaskTwo.Models;

namespace TaskTwo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {}
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubTask>()
                .HasOne(st => st.Task)
                .WithMany(t => t.SubTasks)
                .HasForeignKey(st => st.TaskId);
        }

    }
}
