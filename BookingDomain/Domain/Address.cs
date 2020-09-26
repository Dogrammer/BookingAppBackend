using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class Address : BaseEntity
    {
        public string StreetNameAndNumber { get; set; }
        public long CityId { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public long CountryId { get; set; }
    }
}
