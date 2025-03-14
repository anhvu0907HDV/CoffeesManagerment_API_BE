﻿using Assignment_PRN231_API.DTOs.Account;
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.DTOs.Shop;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Assignment_PRN231_API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityRole, RoleDto>().ReverseMap();
            CreateMap<AppUser, ListManagerDto>()
                .ForMember(dest => dest.NameInventory, opt => opt.MapFrom(src =>
                    string.Join(", ", src.UserShops.Select(us => us.Shop.Name))));


            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => GetUserNameFromEmail(src.Email)));



            CreateMap<ShopDto, Shop>().ReverseMap();




            CreateMap<ManagerEditDto, AppUser>()
                .ForMember(dest => dest.Avatar, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => GetUserNameFromEmail(src.Email)))
                .ReverseMap(); 
            CreateMap<ManagerAddDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => GetUserNameFromEmail(src.Email)))
                .ReverseMap();
            CreateMap<AppUser, ManagerDto>()
                 .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Avatar))
                 .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.UserShops.Select(us => us.Shop.ShopId).FirstOrDefault()))
                 .ReverseMap();



            CreateMap<AppUser, StaffDto>()
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.UserShops.Select(us => us.Shop.ShopId).FirstOrDefault()))
                .ReverseMap();
            CreateMap<StaffEditDto, AppUser>()
                .ForMember(dest => dest.Avatar, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => GetUserNameFromEmail(src.Email)));
            CreateMap<AppUser, StaffEditDto>()
                .ForMember(dest => dest.Avatar, opt => opt.Ignore())
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.UserShops.Select(us => us.Shop.ShopId).FirstOrDefault()))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Avatar));
            CreateMap<AppUser, StaffOwnerDto>()
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Avatar))
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.UserShops.Select(us => us.Shop.ShopId).FirstOrDefault()))
                .ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.UserShops.Select(us => us.Shop.Name).FirstOrDefault()))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
                .ReverseMap();
        }

        private object GetUserNameFromEmail(string? email)
        {
            return email!.Split('@')[0];
        }
    }
}
