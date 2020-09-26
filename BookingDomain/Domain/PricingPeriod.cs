using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class PricingPeriod : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTimeOffset DateFrom { get; set; }
        //public DateTimeOffset DateTo { get; set; }
        //public double Price { get; set; }
        //public long ApartmentId { get; set; }
        //public Apartment Apartment { get; set; }

    }
}
