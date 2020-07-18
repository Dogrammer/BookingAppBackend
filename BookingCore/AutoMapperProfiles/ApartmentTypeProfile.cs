﻿using AutoMapper;
using BookingCore.RequestModels;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.AutoMapperProfiles
{
    public class ApartmentTypeProfile : Profile
    {
        public ApartmentTypeProfile()
        {
            CreateMap<CreateApartmentTypeRequest, ApartmentType>();
        }
    }
}