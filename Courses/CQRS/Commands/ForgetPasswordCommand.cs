using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MediatR;
using Courses.Models.Responses;

namespace Courses.CQRS.Commands
{
    public class ForgetPasswordCommand : IRequest<VerificationResponse>
    {
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Invalid email format.")]
        [DefaultValue("john.doe@example.com")]
        public string Email { get; set; }
    }
}
