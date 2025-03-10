using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.CQRS.Handlers
{
    public class DeleteInstructorHandler : IRequestHandler<DeleteInstructorQuery, GeneralResponse>
    {
        private readonly CoursesContext _context;
        public DeleteInstructorHandler(CoursesContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse> Handle(DeleteInstructorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == request.Username);
                if (user is null)
                {
                    return new GeneralResponse
                    {
                        Message = "User is not found"
                    };
                }
                var instructor = await _context.Instructors.SingleOrDefaultAsync(i => i.UserId == user.Id);

                if (instructor is null)
                    return new GeneralResponse { Message = "this Instructor is not found" };

                _context.Instructors.Remove(instructor);

                await _context.SaveChangesAsync(cancellationToken);
                return new GeneralResponse { Message = "This instructor is Deleted", Succeeded = true };
            }
            catch (Exception ex)
            {
                return new GeneralResponse { Message = ex.Message };
            }
        }
    }
}
