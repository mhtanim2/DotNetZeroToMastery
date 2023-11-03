using Microsoft.EntityFrameworkCore;
using PandaIdentity.Models;

namespace PandaIdentity.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
        public DbSet<MyTask> MyTasks { get; set; }
        public DbSet<MySubTask> MySubTasks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ICollection<Status> data = new List<Status>() 
            { 
            new Status(){
            StatusId=Guid.Parse("57c9293e-73f9-43eb-9d8d-2dcfc0d3aa00"),
            StatusType="ToDo"
            },
            new Status(){
            StatusId=Guid.Parse("be0885f8-885c-47bc-bd90-f6d9e4f7f568"),
            StatusType="Doing"
            },
            new Status(){
            StatusId=Guid.Parse("bcd426c5-dd7c-48fd-bab0-673d2c18f50c"),
            StatusType="Done"
            }
            };
            modelBuilder.Entity<Status>().HasData(data);

            ICollection<Priority> PData = new List<Priority>()
            {
            new Priority(){
            PriorityId=Guid.Parse("d9dc7ae1-5ec5-422a-a902-5fc375d29d2d"),
            PriorityType="Easy"
            },
            new Priority(){
            PriorityId=Guid.Parse("d077477f-7aca-4cd0-8dd3-6f01865232c1"),
            PriorityType="Medium"
            },
            new Priority(){
            PriorityId=Guid.Parse("bcd426c5-dd7c-48fd-bab0-673d2c18f50c"),
            PriorityType="Hard"
            }
            };
            modelBuilder.Entity<Priority>().HasData(PData);
        }
    }
}
