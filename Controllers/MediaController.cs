using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController(IMedia _media, UserManager<User> _manager) : ControllerBase
    {
        private readonly IMedia media = _media;
        private readonly UserManager<User> manager = _manager;


        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var resource = await media.UploadImage(file);
                if (resource == null) return NotFound();
                var model = await media.CreateMediaFile(new Dtos.MediaDtos.CreateFile { file = resource });
                return Ok(model.ToFileDto());
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> UploadMultiFiles(IFormFileCollection formFiles, [FromRoute] string userId)
        {
            var user = await manager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound();
            if (user.role == Role.Admin)
            {
                List<string> files = await media.UploadFiles(formFiles);
                ICollection<MediaModel> models = [];
                foreach (string file in files)
                {
                    try
                    {
                        var model = await media.CreateMediaFile(new Dtos.MediaDtos.CreateFile { file = file });
                        models.Add(model);
                    }
                    catch (ValidationException e)
                    {
                        return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                var mediaModel = models.Select(m => m.ToFileDto());
                return Ok(mediaModel);
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
