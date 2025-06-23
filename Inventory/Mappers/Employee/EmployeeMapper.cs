using AutoMapper;
using Inventory.DTO_S;
using Inventory.DTO_S.Account;
using Inventory.Models;

namespace Inventory.Mappers.Employee
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<GetEmployeeDTO, ApplicationUser>()
                .ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest => dest.fisrtName, opt => opt.MapFrom(src => src.fisrtName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.lastName))
                .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.gender))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phoneNumber));
        }
    }
}
