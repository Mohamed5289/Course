using Courses.CQRS.Commands;
using Courses.Data;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.CQRS.Handlers
{
    public class CreateInstructorHandler : IRequestHandler<CreateInstructorCommand, GeneralResponse>
    {
        private readonly CoursesContext context;
        private readonly UserManager<AppUser> userManager;

        public CreateInstructorHandler(CoursesContext context , UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<GeneralResponse> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByNameAsync(request.Username);

                if (user is null  || !user.EmailConfirmed)
                {
                    return new GeneralResponse
                    {
                        Message = "User is not found"
                    };
                }

                await context.Instructors.AddAsync(new Instructor
                {
                    UserId = user.Id,
                    JobTitle = request.JobTitle
                });

                await userManager.AddToRoleAsync(user, "Instructor");
                await context.SaveChangesAsync(cancellationToken);
                return new GeneralResponse { Succeeded = true, Message = "This Instructor Created" };
            }
            catch (Exception ex)
            {
                return new GeneralResponse { Message = ex.Message };
            }

        }
    }
}
