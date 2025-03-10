namespace Courses.Models.Responses
{
    public class MessageResponse
    {
        public string Sender { get; set; }
        public string Revceiver { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
