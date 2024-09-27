using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Controllers;
using Ecommerce.Dtos.User;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly IUser userService = A.Fake<IUser>();
        [Fact]
        public async Task UserController_SignUp_ReturnsCreated()
        {
            RegisterDto dto = A.Fake<RegisterDto>();
            UserController controller = new UserController(userService);
            var result = await controller.CreateAccountAsync(dto);
            result.Should().BeOfType(typeof(CreatedResult));
        }

        [Fact]
        public async Task UserController_Login_ReturnsCreated()
        {
            LoginDto dto = A.Fake<LoginDto>();
            UserController controller = new UserController(userService);
            var result = await controller.Login(dto);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData("userId", 0)]
        public async Task UserController_verifyUser_ReturnsOk(string userId, int code)
        {
            UserController controller = new UserController(userService);
            var result = await controller.verifyUser(userId, code);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Theory]
        [InlineData("userId")]
        public async Task UserController_DeleteUser_ReturnsOk(string userId)
        {
            UserController controller = new UserController(userService);
            var result = await controller.DeleteUser(userId);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}