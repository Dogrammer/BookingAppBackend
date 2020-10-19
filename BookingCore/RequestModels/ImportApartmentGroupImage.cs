using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class ImportApartmentGroupImage
    {
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

    }
}
