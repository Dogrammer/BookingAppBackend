using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreatePricingPeriodDetailRequest
    {
        public long ApartmentId { get; set; }
        public List<PricingPeriodDetail> PricingPeriodDetails { get; set; }

    }
}
