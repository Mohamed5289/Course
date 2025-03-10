using System.Text.Json.Serialization;

namespace Courses.Models.Responses
{
    public class RegisterResponse
    {
        public string Message { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsAuthenticated { get; set; } = false;

        public string Token { get; set; } = string.Empty;

        public List<string> Roles { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; } = string.Empty;

        public DateTime RefreshTokenExpiration { get; set; }
    }
}
