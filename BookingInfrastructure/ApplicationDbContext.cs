using BookingDomain;

namespace BookingInfrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User,Role, int>
    {
    }
}
