namespace Courses.Models.Responses
{
    public class ResetPasswordResponse
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; } = false;
    }
}
