using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MediatR;
using Courses.Models.Responses;

namespace Courses.CQRS.Commands
{
    public class CreateCourseCommand : IRequest<GeneralResponse>
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "Name must be at most 50 characters.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        [DefaultValue("php")]
        public string Name { get; set; }

        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Description can only contain letters and spaces.")]
        [Required(ErrorMessage = "Description is required.")]
        [DefaultValue("Description")]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "InstructorID is required.")]
        public int InstructorID { get; set; }
    }
}
