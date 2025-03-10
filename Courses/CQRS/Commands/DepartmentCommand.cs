using Courses.Models.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Courses.CQRS.Commands
{
    public class DepartmentCommand : IRequest<GeneralResponse>
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "Name must be at most 50 characters.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        [DefaultValue("CS")]
        public string Name { get; set; }

        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Description can only contain letters and spaces.")]
        [Required(ErrorMessage = "Description is required.")]
        [DefaultValue("Description")]
        public string Description { get; set; } = string.Empty;

    }
}
