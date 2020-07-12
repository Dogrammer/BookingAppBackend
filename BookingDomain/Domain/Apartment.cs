using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class Apartment : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public double Size { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public long ApartmentTypeId { get; set; }

        public PricingPeriod PricingPeriod { get; set; }
        public long PricingPeriodId { get; set; }

    }
}
