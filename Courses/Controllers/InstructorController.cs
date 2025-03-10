using Courses.CQRS.Commands;
using Courses.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertInstructor (CreateInstructorCommand command)
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteInstructor([FromQuery]DeleteInstructorQuery command)
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

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetInstructors()
        {
            var users = await _mediator.Send(new GetInstructorsQuery());

            if (users.Count == 0)
                return BadRequest(new { Message = "Not Found Instructors to System" });

            return Ok(users);
        }

    }
}
