using Courses.CQRS.Commands;
using Courses.Data;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Handlers
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, GeneralResponse>
    {
        private readonly CoursesContext coursesContext;

        public CreateCourseHandler (CoursesContext coursesContext)
        {
            this.coursesContext = coursesContext;
        }

        public async Task<GeneralResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = new Course
                {
                    Name = request.Name,
                     Description = request.Description,
                    InstructorID = request.InstructorID
                };
                await coursesContext.AddAsync (course, cancellationToken);

                var result = await coursesContext.SaveChangesAsync (cancellationToken);

                return new GeneralResponse { Succeeded = true, Message = "This Course is added" };
            }
            catch (Exception ex)
            {
                return new GeneralResponse { Message = ex.Message };
            }
        }
    }
}
