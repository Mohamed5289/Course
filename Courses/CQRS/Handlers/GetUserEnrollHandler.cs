using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class GetUserEnrollHandler : IRequestHandler<EnrollQuery , List<EnrollResponse>?>
    {
        private readonly CoursesContext _context;
        public GetUserEnrollHandler(CoursesContext context)
        {
            _context = context;
        }

        public async Task<List<EnrollResponse>?> Handle(EnrollQuery request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                    .SingleOrDefaultAsync(c => c.ID == request.CourseId, cancellationToken);

            // Check if course is null
            if (course is null)
                return null;

            // Map users to response DTOs
            var users = course.Enrollments?.Select(u => new EnrollResponse
            {
                Username = u.User!.UserName!,
                Course = u.Course!.Name,
                Status = u.Status
            }).ToList();


            return users;
        }
    }
}
