using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServer.Controllers.Tests
{
    [TestClass()]
    public class AuthControllerTests
    {
        private readonly UserService _userService;
        private readonly User user = new User();


        [TestMethod()]
        public async Task RegisterTestAsync()
        {

            var result = new AuthController(_userService).Register(user);
            Assert.AreEqual(typeof(Task<IActionResult>), result.GetType());
        }

        [TestMethod()]
        public async Task LoginTestAsync()
        {
            var result = new AuthController(_userService).Login(user);
            //Console.Write(result.GetType());
            //Assert.IsInstanceOfType(typeof(Task<IActionResult>), result.GetType());
            Assert.AreEqual(typeof(Task<IActionResult>), result.GetType());
        }
    }
}