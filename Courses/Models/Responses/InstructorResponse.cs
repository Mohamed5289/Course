namespace Courses.Models.Responses
{
    public class InstructorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Email { get; set; }
        public decimal? Salary { get; set; }
        public string? JobTitle { get; set; }
    }
}
