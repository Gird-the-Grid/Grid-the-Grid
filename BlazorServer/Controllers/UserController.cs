using BlazorServerAPI.Repository;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userService;

        public UserController(UserRepository userService)
        {
            _userService = userService;
            //TODO: Admin access to manage users
            //Deny access to users that are not admins
        }

        [HttpGet]
        public async Task<IActionResult> DenyAccess ()
        {
            var x = await _userService.Get();
            return Ok(x);
        }

    }
}
