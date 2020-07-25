using BookingCore.Services;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.ViewModels
{
    public class ApartmentDetailViewModel
    {
        public ApartmentGroup ApartmentGroup { get; set; }
        public List<string> Images { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public double Size { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public long ApartmentTypeId { get; set; }
        public long ApartmentGroupId { get; set; }
        public Location Location { get; set; }
        public long LocationId { get; set; }

    }
}
