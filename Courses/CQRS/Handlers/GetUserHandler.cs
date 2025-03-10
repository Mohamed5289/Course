using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly CoursesContext context;

        public GetUserHandler(CoursesContext context)
        {
            this.context = context;
        }

        public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await context.Users
                .Select(u => new UserResponse
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    UserName = u.UserName!,
                    IsEmailConfirmed = u.EmailConfirmed

                })
                .ToListAsync(cancellationToken);
            return users;

        }
    }
}
