using BookingDomain.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain
{
    public class User : IdentityUser<int>
    {
        public string Role { get; set; }
        public ICollection<UserApartmentGroup> UserApartmentGroups { get; set; } = new HashSet<UserApartmentGroup>();

    }
}
