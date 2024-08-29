using Ecommerce.Controllers;
using Ecommerce.Dtos.Category;
using Ecommerce.Models;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Test.Controllers
{
    public class CategoryControllerTest
    {
        private readonly ICategory categoryService = A.Fake<ICategory>();


        [Fact]
        public async void CategoryController_GetCategories_ReturnsOK()
        {
            // arrange
            
            var controller = new CategoryController(categoryService);

            //act
            var categories = await controller.GetAllCategories();

            //assert
            categories.Should().NotBeNull();
            categories.Should().BeOfType(typeof (OkObjectResult));
        }

        [Fact]
        public async void CategoryController_CreateCategory_ReturnCreated()
        {
            
            var dto = A.Fake < CreateCategoryDto>();

            var controller = new CategoryController(categoryService);

            var result = await controller.CreateCategory(dto);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(CreatedResult));
        }

        [Theory]
        [InlineData(1,2)]
        public async void CategoryController_GetCategory_ReturnsOk(int a , int excepcted)
        {
            var controller = new CategoryController(categoryService);

            var result = await controller.GetCategory(excepcted);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData(1,2)]
        public async void CategoryController_UpdateCategory_ReturnsOk(int id , int excpected)
        {
            var dto = A.Fake<CreateCategoryDto>();

            var controller = new CategoryController(categoryService);

            var result = await controller.UpdateCategory(id,dto);


            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData(1,2)]
        public async void CategoryController_DeleteCategory_ReturnsOk(int id , int excpected)
        {
            var controller = new CategoryController(categoryService);

            var result = await controller.DeleteCategory(excpected);

            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
