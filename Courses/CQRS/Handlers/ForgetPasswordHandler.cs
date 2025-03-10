using Courses.CQRS.Commands;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.CQRS.Handlers
{
    public class ForgetPasswordHandler : IRequestHandler<ForgetPasswordCommand, VerificationResponse>
    {
        private readonly UserManager<AppUser> userManager;

        public ForgetPasswordHandler(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<VerificationResponse> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return new VerificationResponse { Message = "Email not found." };

            var code = await userManager.GeneratePasswordResetTokenAsync(user);

            return new VerificationResponse
            {
                Message = "Email is Valid ",
                VerificationCode = code,
                Succeeded = true
            };


        }
    }
}
