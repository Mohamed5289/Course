using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Queries
{
    public class GetUsersCourseQuery : IRequest<List<UserResponse>>
    {
        public int Id { get; set; }

        public GetUsersCourseQuery(int id)
        {
            Id = id;
        }
    }
}
