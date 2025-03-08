using AutoMapper;
using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Domain.Entities;

namespace EmployeeMaintenance.Application.Mappings
{
    public class MapperProfileConfig : Profile
    {
        public MapperProfileConfig()
        {
            #region Dto to Command

            CreateMap<UserRequestDto, CreateUserCommand>();
            CreateMap<AddressRequestDto, UserAddressCommand>();

            #endregion Dto to Command

            #region Entity to Dto

            CreateMap<Employee, EmployeeResponseDto>();
            CreateMap<User, UserResponseDto>();
            CreateMap<Address, AddressResponseDto>();
            CreateMap<Department, DepartmentResponseDto>();

            #endregion Entity to Dto
        }
    }
}