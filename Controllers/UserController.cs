using Ecommerce.Dtos.User;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Ecommerce.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class UserController(IUser userRepo) : ControllerBase
    {
        private readonly IUser _userRepo = userRepo;

        [HttpPost("SignUp")]
        [EnableRateLimiting(policyName: "fixed")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await _userRepo.CreateUser(dto);
                if (user == null) return BadRequest("User credentials are invalid!");
                return Created();
            }
            catch (ValidationException e)
            {

                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost("SignIn")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await _userRepo.login(dto);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
