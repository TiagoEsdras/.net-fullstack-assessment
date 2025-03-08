using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;

namespace EmployeeMaintenance.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResposeDto> CreateEmployee(EmployeeRequestDto employeeRequest);
    }
}