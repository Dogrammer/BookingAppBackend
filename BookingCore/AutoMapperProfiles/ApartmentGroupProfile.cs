using AutoMapper;
using BookingCore.RequestModels;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.AutoMapperProfiles
{
    public class ApartmentGroupProfile : Profile
    {
        public ApartmentGroupProfile()
        {
            CreateMap<CreateApartmentGroupRequest, ApartmentGroup>();
        }
    }
}
