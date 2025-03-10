using Courses.CQRS.Commands;
using Courses.Data;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class UserCourseHandler : IRequestHandler<UserCourseCommand, GeneralResponse>
    {
        private readonly CoursesContext context;
        private readonly UserManager<AppUser> userManager;
        public UserCourseHandler(CoursesContext coursesContext , UserManager<AppUser> userManager)
        {
            this.context = coursesContext;
            this.userManager = userManager;
        }
        public async Task<GeneralResponse> Handle(UserCourseCommand request, CancellationToken cancellationToken)
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

                await context.Set<CourseUser>().AddAsync(new CourseUser { UserID = user.Id , CourseID = request.CourseId });

                await context.SaveChangesAsync(cancellationToken);

                return new GeneralResponse { Succeeded = true , Message ="This User inserted in this course" };
            }
            catch (Exception ex)
            {
                return new GeneralResponse { Message= ex.Message };
            }
        }
    }
}
