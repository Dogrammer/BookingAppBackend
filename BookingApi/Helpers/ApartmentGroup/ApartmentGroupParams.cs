using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Helpers.ApartmentGroup
{
    public class ApartmentGroupParams : PaginationParams
    {
        //public string CurrentUsername { get; set; }
        public int UserId { get; set; }
        //public int MyProperty { get; set; }
        //public string Gender { get; set; }
        //public int MinAge { get; set; } = 18;
        //public int MaxAge { get; set; } = 150;
        public string OrderBy { get; set; } = "lastActive";
    }
}
