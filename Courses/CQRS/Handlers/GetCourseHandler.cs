using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class GetCourseHandler : IRequestHandler<GetCourseQuery, List<CourseResponse>>
    {
        private readonly CoursesContext _context;
        public GetCourseHandler(CoursesContext context)
        {
            _context = context;
        }
        public async Task<List<CourseResponse>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Department)
                .Select(c => new CourseResponse
                {
                    Id = c.ID,
                    Nmae = c.Name,
                    InstructorName = c.Instructor!.User!.FirstName,
                    DepartureName = c.Department!.Name,
                })
                .ToListAsync(cancellationToken);

            return courses;
        }
    }
}
