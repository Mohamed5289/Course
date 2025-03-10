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
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, bool>
    {
        private CoursesContext _context;
        private Token token;
        private UserManager<AppUser> userManager;

        public RevokeTokenHandler(CoursesContext context, Token token, UserManager<AppUser> userManager)
        {
            _context = context;
            this.token = token;
            this.userManager = userManager;
        }
        public async Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == request.Token));

            if (user is null) return false;

            var refreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == request.Token)!;

            if (!refreshToken.IsActive) return false;

            refreshToken.Revoked = DateTime.UtcNow;

            await userManager.UpdateAsync(user);

            return true;
        }
    }
}
