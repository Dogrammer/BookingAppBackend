using AutoMapper;
using BookingCore.RequestModels;
using BookingDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.AutoMapperProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<CreateUserRegisterRequest, User>();
            CreateMap<CreateUserLoginRequest, User>();
            //CreateMap<CreateEducationLevelRequest, EducationLevel>();
            //CreateMap<EducationLevel, EducationLevelViewModel>();
        }

    }
}
