using System.Text.Json.Serialization;

namespace Courses.Models.Helpers
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireOfDay { get; set; }
    }
}
