using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }

    }
}
