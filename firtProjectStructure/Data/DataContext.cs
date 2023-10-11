using firtProjectStructure.Models;
using Microsoft.EntityFrameworkCore;

namespace firtProjectStructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {}
        public DbSet<Category> Categories { get; set; }

    }
}
