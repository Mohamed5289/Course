using Courses.Models.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Courses.CQRS.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserResponse?>
    {
        public string Username { get; set; }

        public GetUserByUsernameQuery( string username)
        {
            Username = username;
        }
    }
}
