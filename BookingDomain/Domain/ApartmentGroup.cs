using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class ApartmentGroup : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ImageFilePath { get; set; }

        //public ICollection<UserApartmentGroup> UserApartmentGroups { get; set; } = new HashSet<UserApartmentGroup>();

    }
}
