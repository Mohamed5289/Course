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
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RegisterResponse>
    {
        private CoursesContext _context;
        private Token token;
        private UserManager<AppUser> userManager;

        public RefreshTokenHandler(CoursesContext context, Token token, UserManager<AppUser> userManager)
        {
            _context = context;
            this.token = token;
            this.userManager = userManager;
        }
        public async Task<RegisterResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == request.Token));

                if (user is null) return new RegisterResponse { Message = "Token not found" };

                var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);

                if (!refreshToken.IsActive) return new RegisterResponse { Message = "Token is not active" };

                refreshToken.Revoked = DateTime.UtcNow;

                var newRefreshToken = token.GeneratorRefreshToken();
                user.RefreshTokens.Add(newRefreshToken);
                await userManager.UpdateAsync(user);

                var jwtToken = await token.CreateToken(user);


                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return new RegisterResponse 
                {
                    IsAuthenticated = true,
                    Email = user.Email!,
                    Username = user.UserName!,
                    Token = jwtToken,
                    RefreshToken = newRefreshToken.Token,
                    RefreshTokenExpiration = newRefreshToken.Expires,
                    Roles = (await userManager.GetRolesAsync(user)).ToList(),
                    Message = "Token revoked" 
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
