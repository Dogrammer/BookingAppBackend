using BookingDomain.DomainBaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingDomain.Domain
{
    public class Country : BaseEntity, IActiveFromToEntity
    {
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTimeOffset ActiveFrom { get; set; }
        public DateTimeOffset? ActiveTo { get; set; }
       
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
