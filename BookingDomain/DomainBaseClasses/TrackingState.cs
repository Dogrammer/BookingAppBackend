using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.DomainBaseClasses
{
    public enum TrackingState
    {
        //
        // Summary:
        //     Existing entity that has not been modified.
        Unchanged = 0,
        //
        // Summary:
        //     Newly created entity.
        Added = 1,
        //
        // Summary:
        //     Existing entity that has been modified.
        Modified = 2,
        //
        // Summary:
        //     Existing entity that has been marked as deleted.
        Deleted = 3
    }
}
