using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController(IMedia _media) : ControllerBase
    {
        private readonly IMedia media = _media;


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var resource = await media.UploadImage(file);
                if (resource == null) return NotFound();
                var model = await media.CreateMediaFile(new Dtos.Media.CreateFile { file = resource });
                return Ok(model.ToFileDto());
            }
            catch(ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }catch(Exception e)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
