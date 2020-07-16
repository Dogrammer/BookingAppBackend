using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreateApartmentGroupRequest
    {
        //public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserApartmentGroup> UserApartmentGroups { get; set; } = new HashSet<UserApartmentGroup>();
    }
}
