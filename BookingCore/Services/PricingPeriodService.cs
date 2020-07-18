using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class PricingPeriodService : Service<PricingPeriod>, IPricingPeriodService
    {
        public PricingPeriodService(ITrackableRepository<PricingPeriod> repository) : base(repository)
        {

        }


    }
}
