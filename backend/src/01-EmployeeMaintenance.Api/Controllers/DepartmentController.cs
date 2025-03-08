using EmployeeMaintenance.Api.Converters;
using EmployeeMaintenance.Application.Queries.Departments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IActionResultConverter _actionResultConverter;

        public DepartmentController(IMediator mediator, IActionResultConverter actionResultConverter)
        {
            _mediator = mediator;
            _actionResultConverter = actionResultConverter;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var result = await _mediator.Send(new GetDepartmentsQuery());
            return _actionResultConverter.Convert(result);
        }
    }
}