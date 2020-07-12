using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class UserApartmentGroup : BaseEntity
    {
        public int UserId { get; set; }
        public int ApartmentGroupId { get; set; }
        public User User { get; set; }
        public ApartmentGroup ApartmentGroup { get; set; }
    }
}
