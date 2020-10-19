using AutoMapper;
using BookingCore.RequestModels;
using BookingCore.ViewModels;
using BookingDomain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.AutoMapperProfiles
{
    public class ApartmentProfile : Profile
    {
        public ApartmentProfile()
        {
            CreateMap<CreateApartmentRequest, Apartment>();
            CreateMap<Apartment, ApartmentDetailViewModel>();
            //CreateMap<Apartment, ApartmentListViewModel>()
               //.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.))
            //.ForMember(dest => dest.LongName, opt => opt.MapFrom(src => (src.KindergardenLivingRoom.KindergardenLocation != null ? src.KindergardenLivingRoom.KindergardenLocation.Name + " - " : "") + (src.KindergardenLivingRoom != null ? src.KindergardenLivingRoom.Name + " - " : "") + src.Name));

        }
    }
}
