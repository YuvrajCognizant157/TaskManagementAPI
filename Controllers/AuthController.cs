using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepo;
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            userRepo = userRepository;
        }


        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginDTO logindto)
        {
            //take information in form (name,password)
            //create a jwt token
            // return the jwt token

            if (!ModelState.IsValid)
                return BadRequest("Incomplete input");

            //verify the user
            User loggedinUser = userRepo.GetUserByName(logindto.name, logindto.password);

            if (loggedinUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            //jwt token creation
            string token = _tokenService.GenerateJwtToken(loggedinUser);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromForm] RegisterDTO registerdto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Incomplete input");

            int status = userRepo.AddUser(registerdto);
            if (status == 0)
            {
                return StatusCode(500);
            }

            return Ok("User added!!!Hurray");
        }
    }
}
