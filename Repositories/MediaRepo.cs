using Ecommerce.Data;
using Ecommerce.Dtos.MediaDtos;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;

namespace Ecommerce.Repositories
{
    public class MediaRepo
        (
        ApplicationDBContext _context,
        [FromKeyedServices("media")] IValidator<CreateFile> _validator
        ) : IMedia
    {

        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateFile> validator = _validator;

        public async Task<MediaModel> CreateMediaFile(CreateFile file)
        {
            var result = validator.Validate(file);
            if (result.IsValid)
            {
                var model = file.ToFile();
                await context.media.AddAsync(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (file.Length == 0 || file == null)
            {
                throw new Exception("file Not Found!");
            }
            var AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".jfif" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(extension))
            {
                throw new Exception("Invalid file format. Only JPG, JPEG, and PNG files are allowed.");
            }
            string fileName = Path.GetFileName(file.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            string filePath = Path.Combine("Media", uniqueFileName);

            try
            {
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return uniqueFileName;
        }

        public async Task<List<string>> UploadFiles(IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                throw new Exception("No files were uploaded.");
            }

            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".jfif" };

            var uploadedFileNames = new List<string>();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new Exception($"Invalid file format. Only {string.Join(", ", allowedExtensions)} files are allowed.");
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine("Media", uniqueFileName);

                try
                {
                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    uploadedFileNames.Add(uniqueFileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return uploadedFileNames;
        }
    }
}
