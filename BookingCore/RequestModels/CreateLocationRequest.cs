using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreateLocationRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long CityId { get; set; }
    }
}
