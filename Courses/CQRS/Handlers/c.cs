using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class GetDepartmentHandler : IRequestHandler<GetDepartments, List<DepartmentResponse>>
    {
        private readonly CoursesContext context;
        public GetDepartmentHandler(CoursesContext context) 
        {
            this.context = context;
        }
        public async Task<List<DepartmentResponse>> Handle(GetDepartments request, CancellationToken cancellationToken)
        {
            var Departments = await context.Departments.Select(d => new DepartmentResponse
            {
                Name = d.Name,
                Description = d.Description ?? string.Empty,
            }).ToListAsync();

            return Departments;
        }
    }
}
