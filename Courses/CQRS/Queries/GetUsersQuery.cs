using Azure.Core;
using Courses.Models.Responses;
using MediatR;

namespace Courses.CQRS.Queries
{
    public record GetUsersQuery : IRequest<List<UserResponse>>;

}
