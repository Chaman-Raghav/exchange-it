using MediatR;

namespace Api.Controllers
{
    internal class GetUsersQuery : IRequest<object>
    {
        public string Id { get; set; }
        public object UserId { get; set; }
    }
}
