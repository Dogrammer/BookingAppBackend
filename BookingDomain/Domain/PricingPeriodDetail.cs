using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class PricingPeriodDetail : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public long Price { get; set; }
        //public PricingPeriod PricingPeriod { get; set; }
        //public long PricingPeriodId { get; set; }

        public long ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public bool IsActive { get; set; }
    }
}
