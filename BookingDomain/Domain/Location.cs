using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace BookingDomain.Domain
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long CityId { get; set; }
        public City City { get; set; }
    }
}
