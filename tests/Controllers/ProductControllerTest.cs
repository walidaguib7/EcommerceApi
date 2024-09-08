using Ecommerce.Controllers;
using Ecommerce.Dtos.Products;
using Ecommerce.Filters;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace tests.Controllers
{
    public class ProductControllerTest
    {
        private readonly IProduct productService = A.Fake<IProduct>();
        [Fact]
        public async Task ProductController_GetProducts_ReturnsOk()
        {
            QueryFilters query = A.Fake<QueryFilters>();
            ProductsController controller = new ProductsController(productService);
            var result = await controller.GetProducts(query);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Theory]
        [InlineData("1")]
        public async Task ProductController_GetProductsByUserId_ReturnsOk(string userId)
        {
            QueryFilters query = A.Fake<QueryFilters>();
            ProductsController controller = new ProductsController(productService);
            var result = await controller.GetProducts(userId, query);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public async Task ProductController_AddProduct_ReturnsCreated()
        {
            CreateProductDto dto = A.Fake<CreateProductDto>();
            ProductsController controller = new ProductsController(productService);
            var result = await controller.AddProduct(dto);
            result.Should().BeOfType(typeof(CreatedResult));
        }
        [Theory]
        [InlineData(1)]
        public async Task productsController_UpdateProdcuts_ReturnsOk(int id)
        {
            UpdateProductDto dto = A.Fake<UpdateProductDto>();
            ProductsController controller = new ProductsController(productService);
            var result = await controller.UpdateProduct(id, dto);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Theory]
        [InlineData(1)]
        public async Task productsController_DeleteProdcuts_ReturnsOk(int id)
        {
            ProductsController controller = new ProductsController(productService);
            var result = await controller.DeleteProduct(id);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}