using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Queries
{
    public record GetDepartments : IRequest<List<DepartmentResponse>>;

}
