using Ecommerce.Dtos.Media;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IMedia
    {
        public Task<MediaModel> CreateMediaFile(CreateFile file);

        public Task<string> UploadImage(IFormFile file);

        public Task<List<string>> UploadProductFiles(IFormCollection files);
    }
}
