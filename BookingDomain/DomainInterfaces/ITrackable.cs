using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDomain.DomainInterfaces
{
    public interface ITrackable
    {
        //
        // Summary:
        //     Change-tracking state of an entity.
        TrackingState TrackingState { get; set; }
        //
        // Summary:
        //     Properties on an entity that have been modified.
        ICollection<string> ModifiedProperties { get; set; }
    }
}
