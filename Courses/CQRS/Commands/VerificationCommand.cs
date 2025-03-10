using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Commands
{
    public class VerificationCommand : IRequest<RegisterResponse>
    {

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Invalid email format.")]
        [DefaultValue("john.doe@example.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Verification code is required.")]
        [DefaultValue("123456")]
        public string VerificationCode { get; set; }
    }
}
