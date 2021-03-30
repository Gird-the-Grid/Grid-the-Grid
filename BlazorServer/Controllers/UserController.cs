using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
            //TODO: Admin access to manage users
        }

        [HttpGet]
        public List<string> DenyAccess ()
        {
            return new List<string>(new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" });
        }

    }
}
