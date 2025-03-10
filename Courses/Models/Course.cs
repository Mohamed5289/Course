namespace Courses.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Description { get; set; }
        public int Hours { get; set; }
        public int? InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public int? DepartmentID { get; set; }
        public virtual Department? Department { get; set; }
        public  virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<CourseUser>? CourseUsers { get; set; }
        public virtual ICollection<AppUser>? Users { get; set; }
        public List<string>? Content { get; set; } = new List<string>();
    }
}
