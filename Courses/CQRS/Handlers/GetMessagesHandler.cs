using Courses.CQRS.Queries;
using Courses.Data;
using Courses.Models;
using Courses.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace Courses.CQRS.Handlers
{
    public class GetMessagesHandler : IRequestHandler<MessageQuery, List<MessageResponse>?>
    {
        private readonly CoursesContext _context;
        private readonly UserManager<AppUser> _userManager;
        public GetMessagesHandler(CoursesContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<List<MessageResponse>?> Handle(MessageQuery request, CancellationToken cancellationToken)
        {
            var sender = await _userManager.FindByNameAsync(request.Sender);
            var receiver = await _userManager.FindByNameAsync(request.Receiver);

            if (sender is null || receiver is null)
            {
                return null;
            }
            var messages = await _context.Messages
            .Where(m => (m.SenderId == sender.Id && m.ReceiverId == receiver.Id) || (m.SenderId == receiver.Id && m.ReceiverId == sender.Id)).Select(m => new MessageResponse
            {
                Sender = m.Sender.UserName!,
                Revceiver = m.Receiver.UserName!,
                Message = m.Content,
                Timestamp = m.Timestamp
            })
            .OrderBy(m => m.Timestamp).ToListAsync();

            return messages;
        }
    }
}
