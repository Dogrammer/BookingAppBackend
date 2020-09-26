using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreateApartmentRequest
    {
        public string Address { get; set; }
        public long ApartmentGroupId { get; set; }
        public long ApartmentTypeId { get; set; }
        public bool BbqTools { get; set; }
        public int Capacity { get; set; }
        public long CityId { get; set; }
        public bool ClimateControl { get; set; }
        public double ClosestBeachDistance { get; set; }
        public double ClosestMarketDistance { get; set; }
        public long CountryId { get; set; }
        public string Description { get; set; }
        public bool KitchenTool { get; set; }
        public string Name { get; set; }
        public int NumberOfBedrooms { get; set; }
        public double Size { get; set; }
        public bool WorkSpace { get; set; }
        public int UserId { get; set; }
        public bool Wifi { get; set; }
        //public List<PricingPeriodDetail> PricingPeriodDetails { get; set; }
        public bool SportTool { get; set; }
    }
}
