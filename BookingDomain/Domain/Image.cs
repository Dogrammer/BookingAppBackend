using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.Domain
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public Apartment Apartment { get; set; }
        public long ApartmentId { get; set; }
        public string FileType { get; set; }

    }
}
