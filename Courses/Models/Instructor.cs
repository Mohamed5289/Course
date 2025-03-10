
using Microsoft.Identity.Client;

namespace Courses.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public virtual AppUser? User { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        public string? JobTitle { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
