using Ecommerce.Dtos.Profiles;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfilesController(IProfiles _profilesRepo) : ControllerBase
    {
        private readonly IProfiles profilesRepo = _profilesRepo;

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDto dto)
        {
            try
            {
                var profile = await profilesRepo.CreateProfile(dto);
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

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetProfile([FromRoute] string userId)
        {
            var profile = await profilesRepo.GetProfile(userId);
            if (profile == null) return NotFound();
            var profileDto = profile.ToProfileDto();
            return Ok(profileDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] int id , [FromBody] UpdateProfileDto dto)
        {
            try
            {
                var profile = await profilesRepo.UpdateProfile(id, dto);
                if (profile == null) return NotFound();
                return Ok("Profile updated");
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
    }
}
