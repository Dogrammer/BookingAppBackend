using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class CityService : Service<City>, ICityService
    {
        public CityService(ITrackableRepository<City> repository) : base(repository)
        {

        }


    }
}
