namespace Courses.Models
{
    public class Enrollment
    {
        public int ID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = "Pending";
        public int? CourseID { get; set; }
        public string? UserID { get; set; }
        public virtual Course? Course { get; set; }
        public virtual AppUser? User { get; set; }
    }
}
