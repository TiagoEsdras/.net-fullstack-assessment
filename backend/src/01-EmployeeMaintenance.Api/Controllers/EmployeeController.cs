using EmployeeMaintenance.Api.Converters;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Interfaces.Services;
using EmployeeMaintenance.Application.Queries.Employees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IMediator _mediator;

        public EmployeeController(IEmployeeService employeeService, IActionResultConverter actionResultConverter, IMediator mediator)
        {
            _employeeService = employeeService;
            _actionResultConverter = actionResultConverter;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequestDto request)
        {
            var result = await _employeeService.CreateEmployee(request);
            return _actionResultConverter.Convert(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _mediator.Send(new GetEmployeesQuery());
            return _actionResultConverter.Convert(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return _actionResultConverter.Convert(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(Guid id)
        {
            var result = await _mediator.Send(new DeleteEmployeeByIdCommand(id));
            return _actionResultConverter.Convert(result);
        }
    }
}