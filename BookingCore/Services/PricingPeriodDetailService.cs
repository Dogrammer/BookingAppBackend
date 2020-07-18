using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class PricingPeriodDetailService : Service<PricingPeriodDetail>, IPricingPeriodDetailService
    {
        public PricingPeriodDetailService(ITrackableRepository<PricingPeriodDetail> repository) : base(repository)
        {

        }

    }
}
