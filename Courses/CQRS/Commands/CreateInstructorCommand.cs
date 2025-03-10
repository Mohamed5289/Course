using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MediatR;
using Courses.Models.Responses;

namespace Courses.CQRS.Commands
{
    public class CreateInstructorCommand : IRequest<GeneralResponse>
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
        [DefaultValue("johndoe123")]
        public string Username { get; set; }

        [Required(ErrorMessage = "JobTitle is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "JobTitle must be between 3 and 30 characters.")]
        [RegularExpression("^[a-zA-Z-_]+$", ErrorMessage = "JobTitle can only contain letters, numbers, and underscores.")]
        [DefaultValue("front-end")]

        public string JobTitle { get; set; }
    }
}
