using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class ApartmentType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
