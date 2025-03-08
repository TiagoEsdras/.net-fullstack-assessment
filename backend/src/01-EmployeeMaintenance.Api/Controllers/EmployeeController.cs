using EmployeeMaintenance.Api.Converters;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IActionResultConverter _actionResultConverter;

        public EmployeeController(IEmployeeService employeeService, IActionResultConverter actionResultConverter)
        {
            _employeeService = employeeService;
            _actionResultConverter = actionResultConverter;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequestDto request)
        {
            var result = await _employeeService.CreateEmployee(request);
            return _actionResultConverter.Convert(result);
        }
    }
}