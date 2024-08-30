

using Ecommerce.Controllers;
using Ecommerce.Dtos.Blocking;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Test.Controllers
{
    public class BlockingControllerTest
    {
        private readonly IBlocking blokingService = A.Fake<IBlocking>();
        [Theory]
        [InlineData("z", "q")]
        public async Task BlockingController_GetBlockedUsers_ReturnOk(string userId, string excpected)
        {
            BlockingController controller = new BlockingController(blokingService);
            var result = await controller.GetBlockedUsers(excpected);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task BlockingController_BlockUser_ReturnsOk()
        {
            BlockUserDto dto = A.Fake<BlockUserDto>();
            BlockingController controller = new BlockingController(blokingService);
            var result = await controller.BlockUser(dto);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task BlockingController_UnBlockUser_ReturnsOk()
        {
            BlockUserDto dto = A.Fake<BlockUserDto>();
            BlockingController controller = new BlockingController(blokingService);
            var result = await controller.UnBlockUser(dto);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}