using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Commands
{
    public record RevokeTokenCommand(string Token) : IRequest<bool>;
}
