using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.CQRS.Handlers
{
    public class GetUserByUserNameHandler : IRequestHandler<GetUserByUsernameQuery, UserResponse?>
    {
        private readonly CoursesContext _context;
        private readonly UserManager<AppUser> _userManager;
        public GetUserByUserNameHandler(CoursesContext context , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserResponse?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null) 
                return null;

            return new UserResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? "email",
                UserName = request.Username,
                IsEmailConfirmed = user.EmailConfirmed
            };
        }
    }
}
