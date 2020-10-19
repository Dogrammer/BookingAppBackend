using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class Apartment : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FullAddress { get; set; }
        public int Capacity { get; set; }
        public double Size { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public long ApartmentTypeId { get; set; }
        public ApartmentGroup ApartmentGroup { get; set; }
        public long ApartmentGroupId { get; set; }
        //public Location Location { get; set; }
        //public long LocationId { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool ClimateControl { get; set; }
        public bool KitchenTool { get; set; }
        public bool Wifi { get; set; }
        public bool BbqTools { get; set; }
        public bool WorkSpace { get; set; }
        public double ClosestBeachDistance { get; set; }
        public double ClosestMarketDistance { get; set; }
        public bool SportTool { get; set; }
        //public Address Address { get; set; }
        //public long AddressId { get; set; }


        //public List<PricingPeriodDetail> PricingPeriodDetails { get; set; }
        public City City { get; set; }
        public long CityId { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        //public Address Address  { get; set; }
        //public long PricingPeriodId { get; set; }

    }
}
