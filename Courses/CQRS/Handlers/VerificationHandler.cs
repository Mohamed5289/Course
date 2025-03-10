using Courses.CQRS.Commands;
using Courses.Data;
using Courses.Models;
using Courses.Models.Helpers;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class VerificationHandler : IRequestHandler<VerificationCommand, RegisterResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly Token token;
        private readonly CoursesContext _context;
        public VerificationHandler(UserManager<AppUser> userManager , Token token, CoursesContext coursesContext) 
        {
            _userManager = userManager;
            this.token = token;
            _context = coursesContext;
        }
        public async Task<RegisterResponse> Handle(VerificationCommand request, CancellationToken cancellationToken)
        {
           var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user is null)
                return new RegisterResponse { Message = "User not found." };
            
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var result = await _userManager.ConfirmEmailAsync(user, request.VerificationCode);

                if (!result.Succeeded)
                {
                    return new RegisterResponse { Message = "Invalid verification code." };
                }

                var tokenString = await token.CreateToken(user);
                var refreshToken = token.GeneratorRefreshToken();

                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "User");

                await transaction.CommitAsync(cancellationToken); 

                return new RegisterResponse
                {
                    Message = "Email confirmed.",
                    Email = request.Email,
                    Username = user.UserName!,
                    Token = tokenString,
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpiration = refreshToken.Expires,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                    IsAuthenticated = true
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken); 
                return new RegisterResponse { Message = ex.Message };
            }


        }
    }
}
