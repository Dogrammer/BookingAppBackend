using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreateApartmentTypeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
