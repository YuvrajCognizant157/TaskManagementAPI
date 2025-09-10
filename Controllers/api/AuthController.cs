using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Controllers.api
{
    public class AuthController : Controller
    {
        private readonly userRepository userRepo;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("")]
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
                { return Unauthorized("Invalid credentials"); }

            //jwt token creation


            return View();
            }
               
        }
    }
}
