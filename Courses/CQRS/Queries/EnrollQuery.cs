using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Queries
{
    public class EnrollQuery : IRequest<List<EnrollResponse>?>
    {
        public int CourseId { get; set; }

        public EnrollQuery(int id) 
        {
            CourseId = id;
        }
    }
}
