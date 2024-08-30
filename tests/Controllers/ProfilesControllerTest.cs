using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Controllers;
using Ecommerce.Dtos.Profile;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Ecommerce.Test.Controllers
{
    public class ProfilesControllerTest
    {
        private readonly IProfile profileService = A.Fake<IProfile>();
        [Fact]
        public async Task ProfilesController_CreateProfile_ReturnsCreated()
        {
            CreateProfileDto dto = A.Fake<CreateProfileDto>();
            ProfilesController controller = new ProfilesController(profileService);
            var result = await controller.CreateProfile(dto);
            result.Should().BeOfType(typeof(CreatedResult));
        }


        [Theory]
        [InlineData(1)]
        public async Task ProfilesController_UpdateProfile_ReturnsCreated(int id)
        {
            UpdateProfileDto dto = A.Fake<UpdateProfileDto>();
            ProfilesController controller = new ProfilesController(profileService);
            var result = await controller.UpdateProfile(id, dto);
            result.Should().BeOfType(typeof(OkObjectResult));
        }


    }
}