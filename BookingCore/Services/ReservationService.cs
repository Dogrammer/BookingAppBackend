using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class ReservationService : Service<Reservation>, IReservationService
    {
        public ReservationService(ITrackableRepository<Reservation> repository) : base(repository)
        {

        }


    }
}
