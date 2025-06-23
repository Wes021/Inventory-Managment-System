using AutoMapper;
using Inventory.DTO_S.Account;
using Inventory.Models;

namespace Inventory.Mappers.Account
{
    public class RegestrationMappers : Profile
    {
        public RegestrationMappers()
        {
            CreateMap<NewUserRegestrationDTO, ApplicationUser>()
                .ForMember(dest => dest.fisrtName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.UserStatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
