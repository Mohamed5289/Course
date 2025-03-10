using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class GetUsersCourseHandle : IRequestHandler<GetUsersCourseQuery, List<UserResponse>>
    {
        private readonly CoursesContext _context;
        public GetUsersCourseHandle(CoursesContext context)
        {
            _context = context;
        }

        public async Task<List<UserResponse>?> Handle(GetUsersCourseQuery request, CancellationToken cancellationToken)
        {
            // Fetch the course with its users
            var course = await _context.Courses
                .SingleOrDefaultAsync(c => c.ID == request.Id, cancellationToken);

            // Check if course is null
            if (course is null)
                return null;

            // Map users to response DTOs
            var users = course.Users?.Select(u => new UserResponse
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email ?? string.Empty,
                UserName = u.UserName ?? string.Empty
            }).ToList();


            return users;
        }
    }
}
