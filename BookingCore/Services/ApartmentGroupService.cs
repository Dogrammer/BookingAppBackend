﻿using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingCore.Services
{
    public class ApartmentGroupService : Service<ApartmentGroup>, IApartmentGroupService
    {
        public ApartmentGroupService(ITrackableRepository<ApartmentGroup> repository) : base(repository)
        {

        }

        //public IQueryable<ApartmentGroup> FilterApartmentGroup(IQueryable<ApartmentGroup> queryable, ApartmentGroupParams apartmentGroupParams)


    }
}
