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
        public IMailService _mailService = new MailService();

        [TestMethod()]
        public async Task RegisterTestAsync()
        {
            
            var authController = new AuthController(_userService, _mailService);
            var request = await authController.Register(user);
            Assert.AreEqual(typeof(ObjectResult), request.GetType());
        }

        [TestMethod()]
        public async Task LoginTestAsync()
        {
            var authController = new AuthController(_userService, _mailService);
            var request = await authController.Login(user);
            Assert.AreEqual(typeof(ObjectResult), request.GetType());
        }
    }
}