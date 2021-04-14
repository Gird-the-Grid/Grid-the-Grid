using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Controllers;
using BlazorServerAPI.Services;

namespace BlazorServer.Controllers.Tests
{
    [TestClass()]
    public class AuthControllerTests
    {
        private readonly UserRepository _userService;
        private readonly User user = new User();
        private readonly IMailService _mailService;

        [TestMethod()]
        public async Task RegisterTestAsync()
        {
            //var result = await new AuthController(_userService).Register(user);
            //Assert.AreEqual(typeof(ObjectResult), result.GetType
            var authController = new AuthController(_userService,_mailService);
            var user = new User("_test", "_test");
            var request = await authController.Register(user);
            Assert.AreEqual(typeof(ObjectResult), request.GetType());
        }

        [TestMethod()]
        public async Task LoginTestAsync()
        {
            var authController = new AuthController(_userService, _mailService);
            var user = new User("_test", "_test");
            var request = await authController.Login(user);
            //Console.Write(request);
            Assert.AreEqual(typeof(ObjectResult), request.GetType());
        }
    }
}