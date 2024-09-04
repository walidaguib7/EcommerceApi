
using Ecommerce.Controllers;
using Ecommerce.Dtos.Comments;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace tests.Controllers
{
    public class CommentControllerTest
    {
        private readonly IComments commentsService = A.Fake<IComments>();

        [Fact]
        public async Task CommentController_GetComments_ReturnsOk()
        {
            CommentsController controller = new CommentsController(commentsService);
            var result = await controller.GetAllComments();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task CommentsController_CreateComment_ReturnsCreated()
        {
            CreateCommentDto dto = A.Fake<CreateCommentDto>();
            CommentsController controller = new CommentsController(commentsService);
            var result = await controller.CreateComment(dto);
            result.Should().BeOfType(typeof(CreatedResult));
        }

        [Theory]
        [InlineData(1)]
        public async Task CommentsController_UpdateComment_ReturnsOk(int id)
        {
            UpdateCommentDto dto = A.Fake<UpdateCommentDto>();
            CommentsController controller = new CommentsController(commentsService);
            var result = await controller.UpdateComment(id, dto);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData(1)]
        public async Task CommentsController_DeleteComment_ReturnsOk(int id)
        {
            CommentsController controller = new CommentsController(commentsService);
            var result = await controller.DeleteComment(id);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}