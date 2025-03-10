using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Commands
{
    public record RefreshTokenCommand(string Token) : IRequest<RegisterResponse>;
}
