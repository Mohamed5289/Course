using Courses.CQRS.Commands;
using Courses.Data;
using Courses.Models.Helpers;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, RegisterResponse>
    {
        private readonly CoursesContext _context;
        private readonly Token token;
        private readonly UserManager<AppUser> userManager;

        public LoginHandler(CoursesContext context, Token token, UserManager<AppUser> userManager)
        {
            _context = context;
            this.token = token;
            this.userManager = userManager;
        }
        public async Task<RegisterResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync( u => u.UserName == request.Username);
                if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
                {
                    return new RegisterResponse { Message = "Error in Username or Password" };
                }

                var isConfirmed = await userManager.IsEmailConfirmedAsync(user);

                if (!isConfirmed)
                {
                    return new RegisterResponse { Message = "Email not confirmed." };
                }

                var tokenString = await token.CreateToken(user);

                if(user.RefreshTokens.Any(u => u.IsActive))
                {
                    var refreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                    await transaction.CommitAsync(cancellationToken);
                    return new RegisterResponse
                    {
                        IsAuthenticated = true,
                        Token = tokenString,
                        RefreshToken = refreshToken!.Token,
                        RefreshTokenExpiration = refreshToken.Expires,
                        Email = user.Email!,
                        Username = user.UserName!,
                        Roles = (await userManager.GetRolesAsync(user)).ToList()
                    };
                }
                else
                {

                    var refreshToken = token.GeneratorRefreshToken();
                    user.RefreshTokens.Add(refreshToken);
                    await userManager.UpdateAsync(user);
                    await transaction.CommitAsync(cancellationToken);
                    return new RegisterResponse
                    {
                        IsAuthenticated = true,
                        Token = tokenString,
                        RefreshToken = refreshToken.Token,
                        RefreshTokenExpiration = refreshToken.Expires,
                        Email = user.Email!,
                        Username = user.UserName!,
                        Roles = (await userManager.GetRolesAsync(user)).ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new RegisterResponse { Message = ex.Message };
            }    
         }
    }
}
