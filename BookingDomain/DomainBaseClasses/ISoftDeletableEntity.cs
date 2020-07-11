using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.DomainBaseClasses
{
    public interface ISoftDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}
