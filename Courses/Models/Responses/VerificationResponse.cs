namespace Courses.Models.Responses
{
    public class VerificationResponse
    {
        public string Message { get; set; } = string.Empty;
        public string VerificationCode { get; set; } = string.Empty;

        public bool Succeeded { get; set; } = false;
    }
}
