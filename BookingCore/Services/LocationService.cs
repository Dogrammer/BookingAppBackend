using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class LocationService : Service<Location>, ILocationService
    {
        public LocationService(ITrackableRepository<Location> repository) : base(repository)
        {

        }


    }
}
