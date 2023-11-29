using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class AuthDataContext : IdentityDbContext
    {
        public AuthDataContext(DbContextOptions<AuthDataContext> options) : base(options)
        {
        }

    }
}
