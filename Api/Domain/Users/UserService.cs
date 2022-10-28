namespace Api.Domain.Users
{
    public class UserService
    {
        private readonly ViceContext _context;

        public UserService(ViceContext context)
        {
            _context = context;
        }

    }
}

