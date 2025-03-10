using Courses.CQRS.Commands;
using Courses.Data;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Handlers
{
    public class InsertDepartmentHandler : IRequestHandler<DepartmentCommand, GeneralResponse>
    {
        private readonly CoursesContext _context;
        public InsertDepartmentHandler(CoursesContext context) 
        {
            _context = context;
        }
        public async Task<GeneralResponse> Handle(DepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Departments.AddAsync(new Department
                {
                    Name = request.Name,
                    Description = request.Description,
                });

                await _context.SaveChangesAsync(cancellationToken);

                return new GeneralResponse { Message = "is Succeeded", Succeeded = true };

            }
            catch (Exception ex)
            {
                return new GeneralResponse { Message = ex.Message };
            }
        }
    }
}
