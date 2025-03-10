using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Queries
{
    public record GetInstructorsQuery : IRequest<List<InstructorResponse>>;

}
