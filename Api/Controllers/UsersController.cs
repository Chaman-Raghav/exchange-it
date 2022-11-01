using System;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Net;
using Api.Domain;
using System.Linq;

namespace Api.Controllers
{
    // TODO: Need to update working of Users Controller.
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IConfiguration Configuration { get; }
        private readonly UserService _userService;
        private object UserId;
        private readonly ViceContext _context;

        public UsersController(IMediator mediator, IConfiguration configuration, UserService userService, ViceContext context)
        {
            _mediator = mediator;
            Configuration = configuration;
            _userService = userService;
            _context = context;
        }

        
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateUsers(User model)
        {
            try
            {
                _context.Users.Add(model);
                _context.SaveChanges();

            }
            catch (UnauthorizedAccessException err)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, err.Message);
            }
            return Ok("Data Inserted");
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                // Return the list of data from the database
                var data = _context.Users.ToList();
                return Ok(data);

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
                var users =  _context.Users.Where(x => x.Id == id).ToList();
                return Ok(users);
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
                var data =  _context.Users.FirstOrDefault(x => x.Id == id);
                if (data != null)
                {
                    _context.Users.Remove(data);
                    _context.SaveChanges();
                   
                }
                return Ok("user got deleted");
            }
            catch (UnauthorizedAccessException err)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, err.Message);
            }
        }

        [HttpPost]
        public ActionResult Update( User model)
        {
            try
            {
                var data = _context.Users.FirstOrDefault(x => x.Id == model.Id);

                // Checking if any such record exist
                if (data != null)
                {
                    data.Id = model.Id;
                    data.Name = model.Name;
                    _context.SaveChanges();
                }
                return Ok("User data Updated");
            }
            catch(UnauthorizedAccessException err)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, err.Message);
            }
        }
    }
}
