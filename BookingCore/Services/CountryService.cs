using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class CountryService : Service<Country>, ICountryService
    {
        public CountryService(ITrackableRepository<Country> repository) : base(repository)
        {

        }


    }
}
