using Api.Domain.Users;
using MediatR;

namespace Api.Controllers
{
    internal class CreateUserCommand : IRequest<User>
    {
        public object User { get; set; }
        public object UserId { get; set; }
    }
}