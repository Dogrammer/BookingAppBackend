using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class ApartmentService : Service<Apartment>, IApartmentService
    {
        public ApartmentService(ITrackableRepository<Apartment> repository) : base(repository)
        {

        }


    }
}
