﻿using BookingCore.Repository;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public class ApartmentTypeService : Service<ApartmentType>, IApartmentTypeService
    {
        public ApartmentTypeService(ITrackableRepository<ApartmentType> repository) : base(repository)
        {

        }

    }
}
