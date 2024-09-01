
using Ecommerce.Dtos.Products;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{

    [ApiController]
    [Route("api/Products")]
    public class ProductsController(IProduct _productService) : ControllerBase
    {
        private readonly IProduct productService = _productService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetProducts();
            var product = products.Select(p => p.ToDto());
            return Ok(product);
        }

        [HttpGet]
        [Route("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts([FromRoute] string userId)
        {
            var products = await productService.GetProducts(userId);
            var product = products.Select(p => p.ToDto());
            return Ok(product);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await productService.GetProduct(id);
            if (product == null) return NotFound();
            return Ok(product.ToDto());
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme , Policy = "admin")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto dto)
        {
            try
            {
                await productService.AddProduct(dto);
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var product = await productService.UpdateProduct(id, dto);
                if (product == null) return NotFound();
                return Ok("Product Updated!");
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = e.Errors.Select(e => e.ErrorMessage)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await productService.DeletePoduct(id);
            if (product == null) return NotFound();
            return Ok("Product deleted!");
        }

    }
}