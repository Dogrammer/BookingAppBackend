using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreateCityRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long CountryId { get; set; }
    }
}
