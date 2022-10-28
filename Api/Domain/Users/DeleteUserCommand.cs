using MediatR;

namespace Api.Controllers
{
    internal class DeleteUserCommand : IRequest<object>
    {
        public string[] Ids { get; set; }
        public object UserId { get; set; }
    }
}
