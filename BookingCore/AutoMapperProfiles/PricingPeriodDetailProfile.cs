using AutoMapper;
using BookingCore.RequestModels;
using BookingCore.ViewModels;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.AutoMapperProfiles
{
    public class PricingPeriodDetailProfile : Profile
    {
        public PricingPeriodDetailProfile()
        {
            CreateMap<CreatePricingPeriodDetailRequest, PricingPeriodDetail>();
            CreateMap<PricingPeriodDetail, PricingPeriodDetailViewModel>();
        }
    }
}
