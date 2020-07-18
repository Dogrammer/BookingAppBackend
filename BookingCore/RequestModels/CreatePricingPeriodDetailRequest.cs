using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreatePricingPeriodDetailRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public long Price { get; set; }
        public long PricingPeriodId { get; set; }
        public bool IsActive { get; set; }

    }
}
