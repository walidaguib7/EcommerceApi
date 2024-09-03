using Ecommerce.Controllers;
using Ecommerce.Models;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Test.Controllers
{
    public class MediaControllerTest
    {
        private readonly IMedia media = A.Fake<IMedia>();

        [Fact]
        public async void MediaController_CreateFile_ReturnsCreated()
        {
            var file = A.Fake<IFormFile>();
            UserManager<User> manager = A.Fake<UserManager<User>>();
            var controller = new MediaController(media, manager);

            var result = await controller.UploadFile(file);

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData("id")]
        public async Task MediaController_CreateFiles_ReturnsOk(string id)
        {
            var files = A.Fake<IFormFileCollection>();
            UserManager<User> manager = A.Fake<UserManager<User>>();
            var controller = new MediaController(media, manager);
            var result = await controller.UploadMultiFiles(files, id);
            result.Should().BeOfType(typeof(OkObjectResult));

        }

        [Theory]
        [InlineData(1)]
        public async Task MediaController_DeleteFile_ReturnsOk(int id)
        {
            UserManager<User> manager = A.Fake<UserManager<User>>();
            var controller = new MediaController(media, manager);
            var result = await controller.DeleteFile(id);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData(1)]
        public async Task MediaController_UpdateFile_ReturnsOk(int id)
        {
            IFormFile formFile = A.Fake<IFormFile>();
            UserManager<User> manager = A.Fake<UserManager<User>>();
            var controller = new MediaController(media, manager);
            var result = await controller.UpdateFile(id, formFile);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

    }
}
