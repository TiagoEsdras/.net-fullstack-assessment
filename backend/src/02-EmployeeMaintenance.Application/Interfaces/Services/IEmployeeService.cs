using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;

namespace EmployeeMaintenance.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDto> CreateEmployee(EmployeeRequestDto employeeRequest);
    }
}