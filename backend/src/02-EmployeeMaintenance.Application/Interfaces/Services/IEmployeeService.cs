using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Shared;

namespace EmployeeMaintenance.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Result<EmployeeResponseDto>> CreateEmployee(EmployeeRequestDto employeeRequest);
    }
}