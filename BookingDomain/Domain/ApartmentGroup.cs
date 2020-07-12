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
        public ICollection<UserApartmentGroup> UserApartmentGroups { get; set; } = new HashSet<UserApartmentGroup>();




    }
}
