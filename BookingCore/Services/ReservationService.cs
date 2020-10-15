using BookingCore.Repository;
using BookingCore.RequestModels;
using BookingDomain.Domain;
using BookingShared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingCore.Services
{
    public class ReservationService : Service<Reservation>, IReservationService
    {
        private readonly IPricingPeriodDetailService _pricingPeriodDetailService;

        public ReservationService(ITrackableRepository<Reservation> repository, IPricingPeriodDetailService pricingPeriodDetailService) : base(repository)
        {
            _pricingPeriodDetailService = pricingPeriodDetailService;
        }

        public bool CheckAvailability(CheckAvailabilityRequest checkAvailabilityRequest)
        {
            bool available = false;
            // ReservationStatusID = 4 = Reserved
            var reservationsForApartment = Repository
                .Queryable()
                .Where(x => !x.IsDeleted && x.ApartmentId == checkAvailabilityRequest.ApartmentId && x.ReservationStatusId == 4).ToList();
            if (reservationsForApartment.Count <= 0)
            {
                available = true;
            }

            foreach (var reservation in reservationsForApartment)
            {
                //requested range is completely before reservation range
                if (checkAvailabilityRequest.DateFrom < reservation.DateFrom && checkAvailabilityRequest.DateTo < reservation.DateFrom)
                {
                    available = true;
                }
                if (checkAvailabilityRequest.DateFrom > reservation.DateTo && checkAvailabilityRequest.DateTo > reservation.DateFrom)
                {
                    available = true;
                }
                if (checkAvailabilityRequest.DateFrom < reservation.DateFrom && checkAvailabilityRequest.DateTo > reservation.DateTo)
                {
                    available = false;
                }
                if (checkAvailabilityRequest.DateFrom.DateTime.InRange(reservation.DateFrom.DateTime, reservation.DateTo.DateTime) && checkAvailabilityRequest.DateTo.DateTime.InRange(reservation.DateFrom.DateTime, reservation.DateTo.DateTime))
                {
                    available = false;
                }
                if (checkAvailabilityRequest.DateFrom < reservation.DateFrom && checkAvailabilityRequest.DateTo.DateTime.InRange(reservation.DateFrom.DateTime, reservation.DateTo.DateTime))
                {
                    available = false;
                }

                if (checkAvailabilityRequest.DateTo > reservation.DateTo && checkAvailabilityRequest.DateFrom.DateTime.InRange(reservation.DateFrom.DateTime, reservation.DateTo.DateTime))
                {
                    available = false;
                }

            }

            return available;
        }

        public double GetPriceForReservation(GetPriceRequest getPriceRequest)
        {
            //izracunaj cijenu
            //dohvati pricing periode detailse za specificni apartman
            double price = 0;
            int counter = 1;

            //var pricingPeriodForApartmant = _pricingPeriodDetailService
            //    .Queryable()
            //    .Where(x => !x.IsDeleted && x.ApartmentId == getPriceRequest.ApartmentId
            //    && getPriceRequest.DateFrom.DateTime.InRange(x.DateFrom.DateTime, x.DateTo.DateTime)
            //    && getPriceRequest.DateTo.DateTime.InRange(x.DateFrom.DateTime, x.DateTo.DateTime)
            //    && (x.DateFrom.DateTime.InRange(getPriceRequest.DateFrom.DateTime, getPriceRequest.DateTo.DateTime)
            //    && x.DateTo.DateTime.InRange(getPriceRequest.DateFrom.DateTime, getPriceRequest.DateTo.DateTime)));


                

            var pricingPeriodForApartment1 = _pricingPeriodDetailService
                .Queryable()
                .Where(x => !x.IsDeleted && getPriceRequest.ApartmentId == x.ApartmentId
                );

            var test = DateTimeOffset.UtcNow <= DateTimeOffset.MaxValue && DateTimeOffset.UtcNow >= DateTimeOffset.MinValue;


            

            var pricingPeriodForApartment2 = pricingPeriodForApartment1
                .Where(x =>
                (getPriceRequest.DateFrom >= x.DateFrom && getPriceRequest.DateFrom <= x.DateTo) || (getPriceRequest.DateTo >= x.DateFrom && getPriceRequest.DateTo <= x.DateTo)
                ||
                (x.DateFrom >= getPriceRequest.DateFrom && x.DateFrom <= getPriceRequest.DateTo && x.DateTo >= getPriceRequest.DateFrom && x.DateTo <= getPriceRequest.DateTo)).ToList();

                                                                                                                        

                



            //
            foreach (var period in pricingPeriodForApartment2)
            {
                if (pricingPeriodForApartment2.Count == 1)
                {
                    var span = (getPriceRequest.DateTo - getPriceRequest.DateFrom).TotalDays;
                    var currentPrice = period.Price * span;
                    price += currentPrice;
                }

                if (pricingPeriodForApartment2.Count == 2)
                {

                    if (counter == 1)
                    {
                        var span = (period.DateTo - getPriceRequest.DateFrom).TotalDays;
                        var currentPrice = period.Price * span;
                        price += currentPrice;
                    }
                    if (counter == 2)
                    {
                        var span = (getPriceRequest.DateTo - period.DateFrom).TotalDays;
                        var currentPrice = period.Price * span;
                        price += currentPrice;
                    }

                    counter++;

                }

                if (pricingPeriodForApartment2.Count > 2)
                {
                    if (getPriceRequest.DateFrom.DateTime.InRange(period.DateFrom.DateTime, period.DateTo.DateTime))
                    {
                        var span = (period.DateTo - getPriceRequest.DateFrom).TotalDays;
                        var currentPrice = period.Price * span;
                        price += currentPrice;
                    }

                    if (getPriceRequest.DateTo.DateTime.InRange(period.DateFrom.DateTime, period.DateTo.DateTime))
                    {
                        var span = (getPriceRequest.DateTo - period.DateFrom).TotalDays;
                        var currentPrice = period.Price * span;
                        price += currentPrice;
                    }

                    if (!getPriceRequest.DateFrom.DateTime.InRange(period.DateFrom.DateTime, period.DateTo.DateTime) && !getPriceRequest.DateTo.DateTime.InRange(period.DateFrom.DateTime, period.DateTo.DateTime))
                    {
                        var span = (period.DateTo - period.DateFrom).TotalDays;
                        var currentPrice = period.Price * span;
                        price += currentPrice;
                    }
                }
            }

            return price;
        }


    }
}
