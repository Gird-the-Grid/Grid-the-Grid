using BlazorServerAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlazorServerAPI.Utils;

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
            var userItems = HttpContext.Items[Text.UserId];
            _ = await _userService.Get();
            return Ok(userItems);
        }

    }
}
