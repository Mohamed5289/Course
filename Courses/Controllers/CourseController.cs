using Courses.CQRS.Commands;
using Courses.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertCourse(CreateCourseCommand command)
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

        [HttpPost("InsertUser")]
        public async Task<IActionResult> InsertUser(UserCourseCommand command)
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

        [HttpGet("GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );

                return BadRequest(errors);
            }

            var users = await _mediator.Send(new GetCourseQuery());

            if (users.Count == 0)
                return BadRequest(new { Message = "Not Found Course to System" });

            return Ok(users);
        }


        [HttpGet("GetUsersCourseById/{id}")]
        public async Task<IActionResult> GetUsersCourseById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );

                return BadRequest(errors);
            }

            var users = await _mediator.Send(new GetUsersCourseQuery(id));

            if ( users is null)
                return BadRequest(new { Message = "this course is not found in System" });
            

            if (users.Count == 0)
                return BadRequest(new { Message = "this course not content on users" });

            return Ok(users);
        }

        [HttpGet("GetUsersEnrollById/{id}")]
        public async Task<IActionResult> GetUsersEnrollById([FromRoute] int id)
        {
            var users = await _mediator.Send(new EnrollQuery(id));

            if (users is null)
                return BadRequest(new { Message = "this course is not found in System" });


            if (users.Count == 0)
                return BadRequest(new { Message = "this course not content on users" });

            return Ok(users);
        }
    }
}
