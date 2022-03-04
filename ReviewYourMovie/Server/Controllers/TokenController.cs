using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewYourMovie.Server.Context;
using ReviewYourMovie.Server.Services;
using System.Security.Claims;

namespace ReviewYourMovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly UserService _userService;

        public TokenController(ILogger<TokenController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        // POST api/Token/register
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserLoginDto login)
        {
            try
            {
                return Ok(await _userService.Register(login));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto auth)
        {
            try
            {
                return Ok(await _userService.Login(auth));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(AuthenticationSchemes = "refresh")]
        [HttpPut("refresh")]
        public IActionResult Refresh()
        {
            Claim? refreshtoken = User.Claims.FirstOrDefault(x => x.Type == "refresh");
            Claim? username = User.Claims.FirstOrDefault(x => x.Type == "username");

            try
            {
                return Ok(_userService.Refresh(username, refreshtoken));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
