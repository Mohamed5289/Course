using Courses.CQRS.Commands;
using Courses.CQRS.Queries;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register (RegisterCommand registerCommand)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );

                return BadRequest(errors);
            }


            var result = await _mediator.Send(registerCommand);

            if(!result.Succeeded)
            {
                return BadRequest(new {Message = result.Message});
            }
            
            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );
                return BadRequest(errors);
            }
            var result = await _mediator.Send(loginCommand);
            if (!result.IsAuthenticated)
            {
                return BadRequest(new { Message = result.Message });
            }

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpGet("Refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken)) return BadRequest(new {Message = "Invalid client request" });


            var result = await _mediator.Send(new  RefreshTokenCommand( refreshToken ));

            if (!result.IsAuthenticated) return BadRequest(new {Message = result.Message });
            
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(new {Message = result});
        }

        [HttpGet("Revoke")]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken)) return BadRequest("Invalid client request");

            var result = await _mediator.Send(new RevokeTokenCommand(refreshToken));

            if (!result) return BadRequest(new {Message = "Invalid client request" });

            return Ok(new {Message = "Token revoked" });
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                IsEssential = true
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerificationCommand verificationCommand)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );
                return BadRequest(errors);
            }
            var result = await _mediator.Send(verificationCommand);

            if (!result.IsAuthenticated)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordCommand forgetPasswordCommand)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );
                return BadRequest(errors);
            }
            var result = await _mediator.Send(forgetPasswordCommand);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = result.Message });
            }
            return Ok(result);
        }

        [HttpPost("RestPassword")]
        public async Task<IActionResult> RestPassword(RestPasswordCommand restPasswordCommand)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );
                return BadRequest(errors);
            }
            var result = await _mediator.Send(restPasswordCommand);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = result.Message });
            }
            return Ok(result);
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());

            if (users.Count == 0)
                return BadRequest(new { Message = "Not Found Users to System" });

            return Ok(users);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery]DeleteUserQuery deleteUser)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );
                return BadRequest(errors);
            }

            var result = await _mediator.Send(deleteUser);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = result.Message });
            }
            return Ok(result);

        }

        [HttpGet("GetUserByUserName/{userName}")]

        public async Task<IActionResult> GetUserByUserName([FromRoute] string userName)
        {
            var pattern = "^[a-zA-Z0-9_]+$";

            if (!Regex.IsMatch(userName, pattern))
                return BadRequest(new { Message = "Username can only contain letters, numbers, and underscores." });

            var user = await _mediator.Send(new GetUserByUsernameQuery(userName));

            if (user is null)
                return BadRequest(new { Message = "User is not found or UserName is not correct" });

            return Ok(user);

        }


        [HttpPost("GetMessages")]
        public async Task<IActionResult> GetMessages(MessageQuery message)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                );
                return BadRequest(errors);
            }

            var result = await _mediator.Send(message);

            if (result is null)
                return BadRequest(new
                {
                    Message = "Send or Receiver is not found "
                });
            if (result.Count == 0)
                return BadRequest(new
                {
                    Message = "Not found Message"
                });
            return Ok(result);
        }
    }
}
