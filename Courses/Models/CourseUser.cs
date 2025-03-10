namespace Courses.Models
{
    public class CourseUser
    {
        public int ID { get; set; }
        public int? CourseID { get; set; }
        public string? UserID { get; set; }
        public virtual AppUser? User { get; set; }
        public  virtual Course? Course { get; set; }
        public int? rating { get; set; }
    }
}
