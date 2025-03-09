using EmployeeMaintenance.Api.Converters;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Services;
using EmployeeMaintenance.Application.Queries.Employees;
using EmployeeMaintenance.Application.Shared;
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
        [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequestDto request)
        {
            var result = await _employeeService.CreateEmployee(request);
            return _actionResultConverter.Convert(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<EmployeeResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees([FromQuery] PaginationRequest pagination)
        {
            var result = await _mediator.Send(new GetEmployeesQuery(pagination));
            return _actionResultConverter.Convert(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return _actionResultConverter.Convert(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployeeById(Guid id)
        {
            var result = await _mediator.Send(new DeleteEmployeeByIdCommand(id));
            return _actionResultConverter.Convert(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployeeDepartment(Guid id, [FromBody] DepartmentRequestDto request)
        {
            var result = await _employeeService.UpdateEmployeeDepartment(id, request);
            return _actionResultConverter.Convert(result);
        }
    }
}