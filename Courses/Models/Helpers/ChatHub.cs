using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System;
using Courses.Data;

namespace Courses.Models.Helpers
{
    public class ChatHub : Hub
    {
        private readonly CoursesContext _context;

        public ChatHub(CoursesContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string receiverUserId, string message)
        {
            string senderUserId = Context.UserIdentifier!; // UserId from claims

            var newMessage = new Message
            {
                SenderId = senderUserId,
                ReceiverId = receiverUserId,
                Content = message
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            // Send message in real-time
            await Clients.User(receiverUserId).SendAsync("ReceiveMessage", senderUserId, message);
        }
    }
}
