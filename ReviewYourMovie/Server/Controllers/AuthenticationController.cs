using Microsoft.AspNetCore.Mvc;
using ReviewYourMovie.Server.Services;
using ReviewYourMovie.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReviewYourMovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private static User user = new();
        private static UserHashPassword userHash = new();


        // POST api/<AuthenticationController>
        [HttpPost("register")]
        public async Task<ActionResult<UserHashPassword>> Register(UserLoginDto login)
        {
            HashPassword.CreatePasswordHash(login.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userHash.Username = login.Username;
            userHash.PasswordHash = passwordHash;
            userHash.PasswordSalt = passwordSalt;

            return Ok(userHash);
        }

        // POST api/<AuthenticationController>
        [HttpPost/*("login")*/]
        public async Task<ActionResult<string>> Login(UserLoginDto login)
        {
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";

            return token;

            //if(userHash.Username != login.Username)
            //{
            //    return BadRequest("User not found");
            //}

            //return Ok("token");

            //return Ok(userHash);
        }

    }
}
