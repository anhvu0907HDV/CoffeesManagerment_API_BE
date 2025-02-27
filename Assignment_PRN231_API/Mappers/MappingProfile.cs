using Assignment_PRN231_API.DTOs.Account;
using Assignment_PRN231_API.DTOs.Owner;
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
        }

        private object GetUserNameFromEmail(string? email)
        {
            return email!.Split('@')[0];
        }
    }
}
