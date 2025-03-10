using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Queries
{
    public record GetCourseQuery :IRequest<List<CourseResponse>>;
}
