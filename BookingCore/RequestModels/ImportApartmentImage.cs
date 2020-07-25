using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BookingCore.RequestModels
{
    public class ImportApartmentImage
    {
        [Required]
        public long ApartmentId { get; set; }
        public IFormFile File { get; set; }

    }
}
