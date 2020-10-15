using BookingCore.RequestModels;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public interface IReservationService : IService<Reservation>
    {
        bool CheckAvailability(CheckAvailabilityRequest checkAvailabilityRequest);
        double GetPriceForReservation(GetPriceRequest getPriceRequest);
    }
}
