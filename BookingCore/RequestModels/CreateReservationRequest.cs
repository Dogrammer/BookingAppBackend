using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreateReservationRequest
    {
        public string Name { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public int ReservationStatusId { get; set; }
        public double TotalPrice { get; set; }
        public long UserId { get; set; }
        public long ApartmentId { get; set; }
    }
}
