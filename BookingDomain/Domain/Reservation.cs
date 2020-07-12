using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class Reservation : BaseEntity
    {
        public string Name { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public double TotalPrice { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public long ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

    }
}
