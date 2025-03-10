using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string SenderId { get; set; } // UserId of the sender

        public string ReceiverId { get; set; } // UserId of the receiver

        public string Content { get; set; } // The message text

        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // When the message was sent

        // Navigation properties
        public virtual AppUser Sender { get; set; }
        public virtual AppUser Receiver { get; set; }
    }
}
