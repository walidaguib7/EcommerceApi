using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Controllers;
using Ecommerce.Dtos.Comments.CommentLikes;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace tests.Controllers
{
    public class CommentLikesControllerTest
    {
        private readonly ICommentLikes commentLikesService = A.Fake<ICommentLikes>();
        [Fact]
        public async Task CommentLikesController_Like_ReturnsCreated()
        {
            CreateCommentLike dto = A.Fake<CreateCommentLike>();
            CommentLikesController controller = new CommentLikesController(commentLikesService);
            var result = await controller.LikeComment(dto);
            result.Should().BeOfType(typeof(CreatedResult));
        }

        [Theory]
        [InlineData("userId", 10)]
        public async Task CommentLikesController_UnLike_ReturnsCreated(string userId, int commentId)
        {
            CommentLikesController controller = new CommentLikesController(commentLikesService);
            var result = await controller.UnLike(commentId, userId);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}