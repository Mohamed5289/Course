using Courses.Models.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Courses.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? SSN { get; set; }
        public string? Gender { get; set; }

        public virtual Instructor? Instructor { get; set; }
        public  virtual ICollection<Course>? Courses { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<CourseUser>? CourseUsers { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
