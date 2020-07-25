using BookingDomain.Domain;
using BookingDomain.DomainBaseClasses;
using BookingDomain.DomainInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingDomain
{
    public class User : IdentityUser<int>, ITrackable
    {
        public string Role { get; set; }
        [NotMapped]
        public TrackingState TrackingState { get; set; }

        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }
        //public ICollection<UserApartmentGroup> UserApartmentGroups { get; set; } = new HashSet<UserApartmentGroup>();

    }
}
