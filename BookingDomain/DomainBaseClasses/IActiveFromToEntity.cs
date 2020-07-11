using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.DomainBaseClasses
{
    public interface IActiveFromToEntity
    {
        bool IsActive { get; set; }
        DateTimeOffset ActiveFrom { get; set; }
        DateTimeOffset? ActiveTo { get; set; }
    }
}
