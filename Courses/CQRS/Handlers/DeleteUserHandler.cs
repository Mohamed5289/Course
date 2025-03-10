using Courses.CQRS.Queries;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.CQRS.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserQuery, GeneralResponse>
    {
        private readonly UserManager<AppUser> userManager;

        public DeleteUserHandler(UserManager<AppUser> userManager )
        {
            this.userManager = userManager;
        }

        public async Task<GeneralResponse> Handle(DeleteUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            if (user is null)
                return new GeneralResponse { Message = "Username is not found " };
            
            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(" ",  result.Errors.Select(e => e.Description));
                return new GeneralResponse { Message = errors };

            }

            return new GeneralResponse
            {
                Message = $"This User {request.Username} is Deleted",
                Succeeded = true
            };
        }
    }
}
