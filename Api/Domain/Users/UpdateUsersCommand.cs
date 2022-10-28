using MediatR;

namespace Api.Controllers
{
    internal class UpdateUsersCommand : IRequest<object>
    {
        public object User { get; set; }
    }
}
