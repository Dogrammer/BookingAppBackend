using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Helpers
{
    public class ApartmentParams : PaginationParams
    {
        //public string CurrentUsername { get; set; }
        //public int UserId { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public int Capacity { get; set; }
        public long CityId { get; set; }
        //public string OrderBy { get; set; } = "lastActive";
    }
}
