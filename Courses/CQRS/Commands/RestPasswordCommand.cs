using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courses.CQRS.Commands
{
    public class RestPasswordCommand : IRequest<ResetPasswordResponse>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Code { get; set; }
    }
}
