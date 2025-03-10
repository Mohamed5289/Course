using Courses.CQRS.Commands;
using Courses.Data;
using Courses.Models;
using Courses.Models.Helpers;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace Courses.CQRS.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, VerificationResponse>
    {
        private readonly CoursesContext _context;
        private readonly Token token;
        private readonly UserManager<AppUser> userManager;

        public RegisterHandler(CoursesContext context , Token token , UserManager<AppUser> userManager)
        {
            _context = context;
            this.token = token;
            this.userManager = userManager;
        }
        public async Task<VerificationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var user = new AppUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    RefreshTokens = new List<RefreshToken>()
                };

                var result = await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return new VerificationResponse { Message = string.Join(" ", errors) };
                }

                var verificationCode = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await transaction.CommitAsync(cancellationToken);

                return new VerificationResponse {Succeeded = true , Message = "Please Confirm the Email", VerificationCode = verificationCode };
                
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                return new VerificationResponse { Message = ex.Message };
            }
        }

    }
}
