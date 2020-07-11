using BookingDomain;
using BookingDomain.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingInfrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User,Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> AuthUsers { get; set; }
        public DbSet<Country> Countries { get; set; }


    }
}
