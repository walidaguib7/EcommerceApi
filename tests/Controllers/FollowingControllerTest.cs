
using Ecommerce.Controllers;
using Ecommerce.Dtos.Followers;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Ecommerce.Test.Controllers
{
    public class FollowingControllerTest
    {
        private readonly IFollowing followingService = A.Fake<IFollowing>();
        [Theory]
        [InlineData("1", "2")]
        public async Task FollowingController_GetFollowers_ReturnsOk(string userId, string excpected)
        {
            FollowingController controller = new FollowingController(followingService);
            var result = await controller.GetFollowers(userId);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData("1", "2")]
        public async Task FollowingController_GetFollowings_ReturnsOk(string userId, string excpected)
        {
            FollowingController controller = new FollowingController(followingService);
            var result = await controller.GetFollowings(userId);
            result.Should().BeOfType(typeof(OkObjectResult));
        }


        [Fact]
        public async Task FollowingController_FollowUser_ReturnsCreated()
        {
            FollowDto dto = A.Fake<FollowDto>();
            FollowingController controller = new FollowingController(followingService);
            var result = await controller.FollowUser(dto);
            result.Should().BeOfType(typeof(CreatedResult));

        }
        [Fact]
        public async Task FollowingController_UnFollowUser_ReturnsCreated()
        {
            FollowDto dto = A.Fake<FollowDto>();
            FollowingController controller = new FollowingController(followingService);
            var result = await controller.UnfollowUser(dto);
            result.Should().BeOfType(typeof(OkObjectResult));

        }
    }
}