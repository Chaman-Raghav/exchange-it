using System;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Api.Controllers
{


    // TODO: Need to update working of Users Controller.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IConfiguration Configuration { get; }
        private readonly UserService _userService;
        private object UserId;

        public UsersController(IMediator mediator, IConfiguration configuration, UserService userService)
        {
            _mediator = mediator;
            Configuration = configuration;
            _userService = userService;
        }


        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var result = await _mediator.Send(new GetUsersQuery { });

                return new OkObjectResult(result);
            }
            catch (UnauthorizedAccessException err)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, err.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id, [FromQuery] bool isUserDeleted)
        {
            try
            {
                var users = await _mediator.Send(new GetUsersQuery { Id = id, UserId = UserId });
                return (ActionResult<User>)users;
            }
            catch (UnauthorizedAccessException err)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, err.Message);
            }
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUser(string id)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand { Ids = new[] { id }, UserId = UserId });

                return id;
            }
            catch (UnauthorizedAccessException err)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, err.Message);
            }
        }
    }
}
