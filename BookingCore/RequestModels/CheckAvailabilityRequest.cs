using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CheckAvailabilityRequest
    {
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public long ApartmentId { get; set; }


    }
}
