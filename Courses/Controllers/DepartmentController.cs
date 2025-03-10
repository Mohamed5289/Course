using Courses.CQRS.Commands;
using Courses.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("InsertDepartment")]
        public async Task<IActionResult> InsertDepartment(DepartmentCommand command)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );

                return BadRequest(errors);
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(result);
        }

        [HttpGet("GetDepartment")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _mediator.Send(new GetDepartments());

            if (departments.Count == 0)
                return BadRequest(new { Message = "Not Found Users to System" });

            return Ok(departments);
        }
    }
}
