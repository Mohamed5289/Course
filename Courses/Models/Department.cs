namespace Courses.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }

    }
}
