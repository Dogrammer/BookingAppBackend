using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreateCountryRequest
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The name of the Country must be between 2 and 50 characters"), MinLength(2)]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "The Description must be between 2 and 250 characters"), MinLength(2)]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset ActiveFrom { get; set; }

        public DateTimeOffset? ActiveTo { get; set; }
    }
}
