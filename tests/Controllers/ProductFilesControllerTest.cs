using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Controllers;
using Ecommerce.Dtos.ProuctFiles;
using Ecommerce.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace tests.Controllers
{
    public class ProductFilesControllerTest
    {
        private readonly IProductFiles productFilesService = A.Fake<IProductFiles>();
        [Theory]
        [InlineData(1)]
        public async Task productFilesController_getAllProductFiles_ReturnsOk(int id)
        {
            ProductFilesController productFiles = new ProductFilesController(productFilesService);
            var result = await productFiles.GetAllProductFiles(id);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData("userId")]
        public async Task productFilesController_CreateProductFiles_ReturnsOk(string userId)
        {
            CreateProductFile productFile = A.Fake<CreateProductFile>();
            ProductFilesController productFiles = new ProductFilesController(productFilesService);
            var result = await productFiles.CreateProductFile(productFile, userId);
            result.Should().BeOfType(typeof(CreatedResult));
        }
    }
}