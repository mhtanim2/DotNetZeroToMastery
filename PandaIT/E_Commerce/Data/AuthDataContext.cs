using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class AuthDataContext : IdentityDbContext
    {
        public AuthDataContext(DbContextOptions<AuthDataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var renderRoleId = "4f10fe73-aa29-44a1-b998-1e46b8cf44aa";
            var writerRoleId = "8b442682-46a5-434d-9de2-bd47a04b4c68";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= renderRoleId,
                    ConcurrencyStamp=renderRoleId,
                    Name="Member",
                    NormalizedName="Member".ToUpper()
                },
                new IdentityRole
                {
                    Id= writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Admin",
                    NormalizedName="Admin".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
