using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployyeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployyeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequestDto request)
        {
            return Ok(await _employeeService.CreateEmployee(request));
        }
    }
}