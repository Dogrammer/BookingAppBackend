using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreatePricingPeriodRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ApartmentId { get; set; }
    }
}
