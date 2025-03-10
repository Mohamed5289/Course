using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class GetInstructorsHandler : IRequestHandler<GetInstructorsQuery, List<InstructorResponse>>
    {
        private readonly CoursesContext _context;
        public GetInstructorsHandler(CoursesContext coursesContext)
        {
            _context = coursesContext;
        }

        public async Task<List<InstructorResponse>> Handle(GetInstructorsQuery request, CancellationToken cancellationToken)
        {
            var instructors = await _context.Instructors.Select(i => new InstructorResponse
            {
                Id = i.Id,
                FirstName = i.User!.FirstName,
                LastName = i.User!.LastName,
                JobTitle = i.JobTitle,
                Email = i.User!.Email,
                HireDate = i.HireDate,
                Salary = i.Salary
            }).ToListAsync(cancellationToken);
            return instructors;
        }
    }
}
