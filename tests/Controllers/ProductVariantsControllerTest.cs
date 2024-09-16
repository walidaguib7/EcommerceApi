using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Controllers;
using Ecommerce.Dtos.Products.Variants;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace tests.Controllers
{
    public class ProductVariantsControllerTest
    {
        private readonly IVariants variantsService = A.Fake<IVariants>();

        [Theory]
        [InlineData(1)]
        public async Task ProductVariantsController_GetAll_ReturnsOk(int id)
        {
            ProductVariantsController controller = new(variantsService);
            var result = await controller.GetAllVariants(id);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData("userId")]
        public async Task ProductVariantsController_CreateVariant_ReturnsCreated(string userId)
        {
            CreateProductVariant variant = A.Fake<CreateProductVariant>();
            ProductVariantsController controller = new(variantsService);
            var result = await controller.CreateVariant(userId, variant);
            result.Should().BeOfType(typeof(CreatedResult));
        }

        [Theory]
        [InlineData(1)]
        public async Task ProductVariantsController_UpdateVariant_ReturnsOk(int id)
        {
            UpdateProductVariant variant = A.Fake<UpdateProductVariant>();
            ProductVariantsController controller = new(variantsService);
            var result = await controller.UpdateVariant(id, variant);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData(1)]
        public async Task ProductVariantsController_DeleteVariant_ReturnsOk(int id)
        {

            ProductVariantsController controller = new(variantsService);
            var result = await controller.DeleteVariant(id);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}