using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Controllers;
using Ecommerce.Dtos.Reviews;
using Ecommerce.Filters;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace tests.Controllers
{
    public class ReviewsControllerTest
    {
        private readonly IReviews reviewRepo = A.Fake<IReviews>();
        [Theory]
        [InlineData(1)]
        public async Task ReviewsController_GetReviews_ReturnsOk(int id)
        {
            QueryFilters query = A.Fake<QueryFilters>();
            ReviewsController controller = new(reviewRepo);
            var result = await controller.GetReviews(id, query);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task ReviewsController_CreateReview_ReturnsCreated()
        {
            CreateReviewDto dto = A.Fake<CreateReviewDto>();
            ReviewsController controller = new(reviewRepo);
            var result = await controller.CreateReview(dto);
            result.Should().BeOfType(typeof(CreatedResult));
        }

        [Theory]
        [InlineData(1)]
        public async Task ReviewController_UpdateReview_ReturnsCreated(int id)
        {
            UpdateReviewDto dto = A.Fake<UpdateReviewDto>();
            ReviewsController controller = new(reviewRepo);
            var result = await controller.UpdateReview(id, dto);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData(1)]
        public async Task ReviewController_DeleteReview_ReturnsCreated(int id)
        {

            ReviewsController controller = new(reviewRepo);
            var result = await controller.DeleteReview(id);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}