﻿using Assignment_PRN231_API.DTOs.Account;
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.Models;
using AutoMapper;

namespace Assignment_PRN231_API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, ListManagerDto>()
                .ForMember(dest => dest.NameInventory, opt => opt.MapFrom(src =>
                    string.Join(", ", src.UserShops.Select(us => us.Shop.Name))));

            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => GetUserNameFromEmail(src.Email)));

            CreateMap<ShopDto, Shop>().ReverseMap();

            CreateMap<ManagerEditDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => GetUserNameFromEmail(src.Email)))
                .ReverseMap();

            CreateMap<AppUser, ManagerDto>()
                 .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.UserShops.Select(us => us.Shop.ShopId).FirstOrDefault()))
                 .ReverseMap();

            CreateMap<AppUser, StaffDto>()
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.UserShops.Select(us => us.Shop.ShopId).FirstOrDefault()))
                .ReverseMap();
        }

        private object GetUserNameFromEmail(string? email)
        {
            return email!.Split('@')[0];
        }
    }
}
