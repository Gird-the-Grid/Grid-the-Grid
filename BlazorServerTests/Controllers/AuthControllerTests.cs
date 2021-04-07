using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
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
            //var result = await new AuthController(_userService).Register(user);
            //Assert.AreEqual(typeof(ObjectResult), result.GetType
            var authController = new AuthController(_userService);
            var user = new User("_test", "_test");
            var request = await authController.Register(user);
            Assert.AreEqual(typeof(ObjectResult), request.GetType());
        }

        [TestMethod()]
        public async Task LoginTestAsync()
        {
            var authController = new AuthController(_userService);
            var user = new User("_test", "_test");
            var request = await authController.Login(user);
            //Console.Write(request);
            Assert.AreEqual(typeof(ObjectResult), request.GetType());
        }
    }
}