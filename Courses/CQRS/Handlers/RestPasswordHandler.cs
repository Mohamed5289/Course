using Courses.CQRS.Commands;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.CQRS.Handlers
{
    public class RestPasswordHandler : IRequestHandler<RestPasswordCommand, ResetPasswordResponse>
    {
        private readonly UserManager<AppUser> userManager;
        public RestPasswordHandler(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<ResetPasswordResponse> Handle(RestPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ResetPasswordResponse { Message = "Email is not found" };
            }

            var result = await userManager.ResetPasswordAsync(user, request.Code, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(" ", result.Errors.Select(e => e.Description));
                return new ResetPasswordResponse { Message = errors };
            }

            return new ResetPasswordResponse
            {
                Succeeded = true,
                Message = "Password is Changed",
                Email = request.Email
            };
        }
    }
}
