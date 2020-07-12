using BookingDomain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingInfrastructure.ModelConfigurations
{
    public class UserApartmentGroupConfiguration : IEntityTypeConfiguration<UserApartmentGroup>
    {
        public void Configure(EntityTypeBuilder<UserApartmentGroup> builder)
        {
            builder.HasKey(pl => new { pl.ApartmentGroupId, pl.UserId });

            builder.HasOne(pl => pl.User)
                   .WithMany(kp => kp.UserApartmentGroups)
                   .HasForeignKey(kp => kp.UserId);

            builder.HasOne(pl => pl.User)
                   .WithMany(kp => kp.UserApartmentGroups)
                   .HasForeignKey(kp => kp.ApartmentGroupId);
        }
    }
}
