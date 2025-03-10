using Courses.CQRS.Commands;
using Courses.Data;
using Courses.Models.Responses;
using Courses.Models;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class EnrollHandler : IRequestHandler<EnrollCommand, GeneralResponse>
    {
        private readonly CoursesContext context;
        private readonly UserManager<AppUser> userManager;
        public EnrollHandler(CoursesContext coursesContext, UserManager<AppUser> userManager)
        {
            this.context = coursesContext;
            this.userManager = userManager;
        }
        public async Task<GeneralResponse> Handle(EnrollCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByNameAsync(request.Username);

                if (user is null)
                {
                    return new GeneralResponse { Message = "User is not found" };
                }

                var course = await context.Courses.SingleOrDefaultAsync(c => c.ID == request.CourseId);

                if (course is null)
                {
                    return new GeneralResponse { Message = "Course is not found" };
                }

                await context.Enrollments.AddAsync(new Enrollment { UserID = user.Id, CourseID = request.CourseId });

                await context.SaveChangesAsync(cancellationToken);

                return new GeneralResponse { Succeeded = true, Message = "This User Enroll in this course" };
            }
            catch (Exception ex)
            {
                return new GeneralResponse { Message = ex.Message };
            }
        }
    }
}
