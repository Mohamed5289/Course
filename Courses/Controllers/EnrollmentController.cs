using Courses.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Enroll")]
        public async Task<IActionResult> InsertUser(EnrollCommand command)
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
    }
}
