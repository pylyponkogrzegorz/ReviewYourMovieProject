using Microsoft.AspNetCore.Mvc;
using ReviewYourMovie.Server.Services;
using ReviewYourMovie.Shared.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewYourMovie.Server.Context;
using System.Security.Claims;
using ReviewYourMovie.Server.Managers;
using Blazored.LocalStorage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReviewYourMovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserContext _context;

        public AuthenticationController(UserContext context)
        {
            _context = context;
        }

        private static User user = new();

        [HttpPost]
        public async Task<ActionResult<User>> Authenticate(UserLoginDto userDto)
        {
            var userFromDb = await _context.Users.FindAsync(userDto.Username);

            return Ok(userFromDb);
        }


        // POST api/Authentication/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserLoginDto login)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(login.Password);

            //var token = TokenManager.GenerateAccessToken(user);

            user.Username = login.Username;
            user.Password = passwordHash;
            user.LastLogonTime = DateTime.Now;
            user.UserRoleId = 3;
            user.RegisterTime = DateTime.Now;
            user.Token = String.Empty;
            user.FirstName = String.Empty;
            user.LastName = String.Empty;
            user.EmailAddress = String.Empty;

            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Username == login.Username);

            if (userFromDb != null)
            {
                return NotFound("This username is taken");
            }

            user.RegisterComplete = true;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("Registered");
        }

        // POST api/<AuthenticationController>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == login.Username);

            if (user == null)
            {
                return NotFound("There is no such User.");
            }

            var validPassword = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

            if (!validPassword)
            {
                return NotFound("Bad password");
            }

            var refreshToken = TokenManager.GenerateRefreshToken(user);

            user.Token.Remove(0);

            user.Token = refreshToken.refreshToken;

            var token = TokenManager.GenerateAccessToken(user);

            //var tokenToDb = JsonConvert.SerializeObject(refreshToken);


            //user.Token = tokenToDb;

            await _context.SaveChangesAsync();

            return token;
        }

    }
}
