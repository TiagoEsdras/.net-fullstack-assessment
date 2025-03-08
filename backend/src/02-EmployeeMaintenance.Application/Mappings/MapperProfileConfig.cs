using AutoMapper;
using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.DTOs.Request;

namespace EmployeeMaintenance.Application.Mappings
{
    public class MapperProfileConfig : Profile
    {
        public MapperProfileConfig()
        {
            CreateMap<UserRequestDto, CreateUserCommand>();
            CreateMap<AddressRequestDto, UserAddressCommand>();
        }
    }
}