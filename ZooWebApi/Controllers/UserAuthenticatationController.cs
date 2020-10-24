using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZooWebApi.Entities;
using ZooWebApi.Models;
using ZooWebApi.Services;

namespace ZooWebApi.Controllers
{
    [Route("v1/Authentication")]
    [ApiController]
    public class UserAuthenticatationController : ControllerBase
    {
        private IUsers _users;
        private IDb _db;
        private IJwtGenerator _jwtGenerator;

        public UserAuthenticatationController(IUsers users, IDb db, IJwtGenerator jwtGenerator)
        {
            _db = db;
            _users = users;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidteUser([FromBody] UserCredentials credentials)
        {
            IActionResult response = Unauthorized("User name or password mismatch");

            List<User> users = await _db.GetUsersFromDb();
            User user = _users.GetUserByName(users, credentials.UserName);

            if (user == null)
            {
                response = BadRequest("Something went wrong");
            }
            else
            {
                if (user.Password == credentials.Password)
                {
                    string token = _jwtGenerator.GenerateJWTToken(credentials);
                    response = Ok(new RegisteredUser { UserName = user.UserName, Token = token });
                }
            }

            return response;
        }
    }
}
